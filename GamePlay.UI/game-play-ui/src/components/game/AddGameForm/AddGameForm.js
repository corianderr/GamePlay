import useAxiosPrivate from "hooks/useAxiosPrivate";
import React, { useState } from "react";
import { toast } from "react-toastify";
import GameForm from "../GameForm/GameForm";
import { useTranslation } from "react-i18next";

const AddGameForm = ({ handleClose, updateData }) => {
  const axiosPrivate = useAxiosPrivate();
  const { t } = useTranslation();

  const handleAdd = async (data) => {
    const response = await axiosPrivate.post("game", data, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
    if (response.data.succeeded) {
      console.log(response);
      handleClose();
      updateData();
      toast.success(t("game.addedMes"));
    } else {
      console.log(response);
      response.data.errors.map((e) => {
        toast.error(e);
      });
    }
  };

  return <GameForm handleLogic={handleAdd} buttonValue={t("forms.add")}/>;
};

export default AddGameForm;
