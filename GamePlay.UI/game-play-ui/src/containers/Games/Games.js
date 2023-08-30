import { useState, useEffect } from "react";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { useNavigate, useLocation } from "react-router-dom";
import GameList from "../../components/game/GameList/GameList";

const Games = () => {
  const [games, setGames] = useState([]);
  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();
  const location = useLocation();

  const getGames = async () => {
    try {
      const response = await axiosPrivate.get("/game");
      console.log(response.data);
      setGames(response.data.result);
    } catch (err) {
      if (err.name === "CanceledError") {
        return;
      }

      console.error(err);
      navigate("/login", { state: { from: location }, replace: true });
    }
  };

  useEffect(() => {
    getGames();
  }, []);

  return (
    <>
      <GameList games={games} header={"Games"} updateGames={getGames} />
    </>
  );
};

export default Games;
