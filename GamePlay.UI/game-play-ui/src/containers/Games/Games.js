import { useState, useEffect } from "react";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { useNavigate, useLocation } from "react-router-dom";
import GameList from "../../components/game/GameList/GameList";

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
        const response = await axiosPrivate.get("/game", {
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
      <GameList games={games} header={"Games"}/>
    </>
  );
};

export default Games;
