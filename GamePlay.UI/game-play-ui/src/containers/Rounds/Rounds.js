import { useState, useEffect } from "react";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { useNavigate, useLocation } from "react-router-dom";
import useAuth from "hooks/useAuth";
import GameRoundTable from "components/game/GameRoundTable/GameRoundTable";

const Rounds = () => {
  const [rounds, setRounds] = useState([]);

  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();

  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    getRounds();
  }, []);

  const getRounds = async () => {
    try {
      const response = await axiosPrivate.get(
        `/gameRound?userId=${auth?.id}`
      );
      if (response.data.succeeded) {
        setRounds(response.data.result);
      }
    } catch (err) {
      if (err.name === "CanceledError") {
        return;
      }

      console.error(err);
      navigate("/login", { state: { from: location }, replace: true });
    }
  };

  return (
    <>
      <GameRoundTable header={"My Rounds"} rounds={rounds} resetRounds={getRounds} />
    </>
  );
};

export default Rounds;
