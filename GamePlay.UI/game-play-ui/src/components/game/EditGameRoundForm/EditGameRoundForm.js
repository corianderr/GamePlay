import useAxiosPrivate from "hooks/useAxiosPrivate";
import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import GameRoundForm from "../GameRoundForm/GameRoundForm";

const EditGameRoundForm = ({ gameRoundId, handleClose }) => {
  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();
  const location = useLocation();

  const [viewModel, setViewModel] = useState();

  useEffect(() => {
    getData();
  }, [gameRoundId]);

  const handleEditRoundResult = async (data) => {
    const response = await axiosPrivate.put(`gameRound/edit/${gameRoundId}`, data);
    if (response.data.succeeded) {
      handleClose();
      toast.success("Round result has been edited");
    } else {
      console.log(response);
      response.data.errors.map((e) => {
        toast.error(e);
      });
    }
  };

  const getData = async () => {
    try {
      const response = await axiosPrivate.get(`/gameRound/edit/${gameRoundId}`);
      console.log(response);
      setViewModel(response.data.result);
      console.log(viewModel);
      console.log(response.data.result);
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
      <GameRoundForm
        viewModel={viewModel}
        submitResult={handleEditRoundResult}
        buttonName={"Edit"}
      />
    </>
  );
};

export default EditGameRoundForm;
