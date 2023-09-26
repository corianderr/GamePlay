import useAxiosPrivate from "hooks/useAxiosPrivate";
import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import GameRoundForm from "../GameRoundForm/GameRoundForm";
import { useTranslation } from "react-i18next";

const EditGameRoundForm = ({ gameRoundId, handleClose }) => {
  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();
  const location = useLocation();
  const { t } = useTranslation();

  const [viewModel, setViewModel] = useState();

  useEffect(() => {
    getData();
  }, [gameRoundId]);

  const handleEditRoundResult = async (data) => {
    const response = await axiosPrivate.put(`gameRound/edit/${gameRoundId}`, data);
    if (response.data.succeeded) {
      handleClose();
      toast.success(t("roundResult.editedMes"));
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
      setViewModel(response.data.result);
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
        submitResult={handleEditRoundResult}
        buttonName={t("forms.edit")}
      />
    </>
  );
};

export default EditGameRoundForm;
