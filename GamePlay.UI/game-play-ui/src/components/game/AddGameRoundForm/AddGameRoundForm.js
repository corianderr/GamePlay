import useAxiosPrivate from "hooks/useAxiosPrivate";
import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import GameRoundForm from "../GameRoundForm/GameRoundForm";

const AddGameRoundForm = ({ gameId, handleClose }) => {
  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();
  const location = useLocation();

  const [viewModel, setViewModel] = useState();

  useEffect(() => {
    getData();
  }, []);

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
      const response = await axiosPrivate.get(`/gameRound/create/${gameId}`);
      setViewModel(response.data.result);
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
        gameId={gameId}
        submitResult={handleAddRoundResult}
        buttonName={"Create"}
      />
    </>
  );
};

export default AddGameRoundForm;
