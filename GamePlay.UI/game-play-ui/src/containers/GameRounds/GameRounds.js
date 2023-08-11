import GameRoundTable from "components/user/GameRoundTable/GameRoundTable";
import useAuth from "hooks/useAuth";
import useAxiosPrivate from "hooks/useAxiosPrivate";
import { useEffect, useState } from "react";
import { useLocation, useNavigate, useParams } from "react-router-dom";

const GameRounds = () => {
    const [rounds, setRounds] = useState([]);
    const { gameId, gameName } = useParams();

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
              `/gameRound?userId=${auth?.id}&gameId=${gameId}`,
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
    <GameRoundTable header={`${gameName} Rounds`} rounds={rounds} />
  )
}

export default GameRounds