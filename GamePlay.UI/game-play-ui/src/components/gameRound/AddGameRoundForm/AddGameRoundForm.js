import useAxiosPrivate from "hooks/useAxiosPrivate";
import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import GameRoundForm from "../GameRoundForm/GameRoundForm";
import { useTranslation } from "react-i18next";

const AddGameRoundForm = ({ gameProp, handleClose }) => {
  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();
  const location = useLocation();
  const { t } = useTranslation();

  const [viewModel, setViewModel] = useState();
  const [games, setGames] = useState([]);
  const [game, setGame] = useState(
    gameProp !== undefined ? gameProp : undefined
  );

  useEffect(() => {
    if (games.length === 0) {
      getGames();
    }

    if (game !== undefined) {
      getData();
    }
  }, [game]);

  const handleAddRoundResult = async (data) => {
    console.log(data);
    try {
      const response = await axiosPrivate.post(`gameRound`, data);
      if (response.data.succeeded) {
        handleClose();
        toast.success(t("roundResult.addedMes"));
      } else {
        response.data.errors.map((e) => {
          toast.error(e);
        });
      }
    } catch (ex) {}
  };

  const getData = async () => {
    try {
      const response = await axiosPrivate.get(`/gameRound/create/${game?.id}`);
      setViewModel(response.data.result);
    } catch (err) {
      if (err.name === "CanceledError") {
        return;
      }

      navigate("/login", { state: { from: location }, replace: true });
    }
  };

  const getGames = async () => {
    try {
      const response = await axiosPrivate.get(`/game`);
      setGames(response.data.result);
      console.log(response.data.result);
    } catch (err) {
      if (err.name === "CanceledError") {
        return;
      }
      navigate("/login", { state: { from: location }, replace: true });
    }
  };

  return (
    <>
      <GameRoundForm
        viewModel={viewModel}
        submitResult={handleAddRoundResult}
        buttonName={t("forms.add")}
        games={games}
        setGame={setGame}
        game={game}
      />
    </>
  );
};

export default AddGameRoundForm;
