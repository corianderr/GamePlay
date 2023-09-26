import GameRoundTable from "components/gameRound/GameRoundTable/GameRoundTable";
import useAuth from "hooks/useAuth";
import useAxiosPrivate from "hooks/useAxiosPrivate";
import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { useLocation, useNavigate, useParams } from "react-router-dom";

const GameRounds = () => {
  const [rounds, setRounds] = useState([]);
  const { gameId, gameName } = useParams();

  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();
  const location = useLocation();
  const { t } = useTranslation();

  useEffect(() => {
    getRounds();
  }, []);

  const getRounds = async () => {
    try {
      let response;
      if (auth?.roles?.includes("admin")){
        response = await axiosPrivate.get(
          `/gameRound?gameId=${gameId}`
        );
      }else {
        response = await axiosPrivate.get(
          `/gameRound?userId=${auth?.id}&gameId=${gameId}`
        );
      }
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
    <GameRoundTable
      header={`${gameName} ${t("roundResult.rounds")}`}
      rounds={rounds}
      resetRounds={getRounds}
    />
  );
};

export default GameRounds;
