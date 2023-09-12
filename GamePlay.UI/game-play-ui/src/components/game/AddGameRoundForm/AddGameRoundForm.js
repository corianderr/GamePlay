import useAxiosPrivate from "hooks/useAxiosPrivate";
import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import GameRoundForm from "../GameRoundForm/GameRoundForm";

const AddGameRoundForm = ({ gameProp, handleClose }) => {
  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();
  const location = useLocation();

  const [viewModel, setViewModel] = useState();
  const [games, setGames] = useState([]);
  const [game, setGame] = useState(
    gameProp !== undefined ? gameProp : undefined
  );

  useEffect(() => {
    if ((games.length === 0)) {
      getGames();
    }

    if (game !== undefined) {
      getData();
    }
  }, [game]);

  const handleAddRoundResult = async (data) => {
    console.log("DATA");
    console.log(data);
    const response = await axiosPrivate.post(`gameRound`, data);
    if (response.data.succeeded) {
      handleClose();
      toast.success("Round result has been added");
    } else {
      response.data.errors.map((e) => {
        toast.error(e);
      });
    }
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
      console.log(response.data.result)
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
        buttonName={"Create"}
        games={games}
        setGame={setGame}
      />
    </>
  );
};

export default AddGameRoundForm;
