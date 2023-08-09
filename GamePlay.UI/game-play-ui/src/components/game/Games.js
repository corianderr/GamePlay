import { useState, useEffect } from "react";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { useNavigate, useLocation } from "react-router-dom";
import "../styles/game.css";

const Games = () => {
  const [games, setGames] = useState([]);
  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    let isMounted = true;
    const controller = new AbortController();

    const getGames = async () => {
      try {
        const response = await axiosPrivate.get("/Game", {
          signal: controller.signal,
        });
        console.log(response.data);
        isMounted && setGames(response.data.result);
      } catch (err) {
        if (err.name === "CanceledError") {
          return;
        }

        console.error(err);
        navigate("/login", { state: { from: location }, replace: true });
      }
    };
    getGames();

    return () => {
      isMounted = false;
      controller.abort();
    };
  }, []);

  return (
    <>
      {games?.length === 0 ? (
        <h5 className="mt-3">There are no games yet..</h5>
      ) : (
        <>
          <div class="container mt-5">
            <div class="row">
              {games.map((game, i) => (
                <div class="col-md-4 col-sm-6 mb-3" key={i}>
                  <div class="card p-3 h-100">
                    <div class="d-flex flex-row mb-3">
                      <img src={game.photoPath} width="70" className="game-cover"/>
                      <div class="d-flex flex-column ms-2">
                        <span>{game.name}</span>
                        <span class="text-black-50">{game.nameRu} ({game.minAge}+)</span>
                        <span class="ratings">
                            {game.averageRating} <i class="fa fa-star"></i>
                        </span>
                      </div>
                    </div>
                    <h6 className="mb-3">
                      From {game.minPlayers} to {game.maxPlayers} participants <br/>
                      Time: {game.minPlayTime} - {game.maxPlayTime} minutes
                    </h6>
                    <div class="d-flex justify-content-between install mt-auto">
                      <span>Released in {game.yearOfRelease}</span>
                      <span class="text-primary">
                        View&nbsp;<i class="fa fa-angle-right"></i>
                      </span>
                    </div>
                  </div>
                </div>
              ))}
            </div>
          </div>
        </>
      )}
    </>
  );
};

export default Games;
