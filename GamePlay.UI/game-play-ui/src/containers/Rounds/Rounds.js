import { useState, useEffect } from "react";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { useNavigate, useLocation } from "react-router-dom";
import useAuth from "hooks/useAuth";
import GameRoundTable from "components/user/GameRoundTable/GameRoundTable";

const Rounds = () => {
  const [rounds, setRounds] = useState([]);

  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();

  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    let isMounted = true;
    const controller = new AbortController();

    const getRounds = async () => {
      try {
        const response = await axiosPrivate.get(
          `/gameRound?userId=${auth?.id}`,
          {
            signal: controller.signal,
          }
        );
        console.log(response.data);
        isMounted && setRounds(response.data.result);
      } catch (err) {
        if (err.name === "CanceledError") {
          return;
        }

        console.error(err);
        navigate("/login", { state: { from: location }, replace: true });
      }
    };
    getRounds();

    return () => {
      isMounted = false;
      controller.abort();
    };
  }, []);

  return (
    <>
      <GameRoundTable header={"My Rounds"} rounds={rounds} />
    </>
  );
};

export default Rounds;
