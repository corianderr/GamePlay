import useAxiosPrivate from "hooks/useAxiosPrivate";
import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";
import GameForm from "../GameForm/GameForm";
import { useLocation, useNavigate } from "react-router-dom";

const EditGameForm = ({ handleClose, gameId, game, updateGame }) => {
  const axiosPrivate = useAxiosPrivate();

  const handleEdit = async (data) => {
    const response = await axiosPrivate.put(`game/${gameId}`, data, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
    if (response.data.succeeded) {
      console.log(response);
      handleClose();
      updateGame();
      toast.success("Game has been edited");
    } else {
      console.log(response);
      response.data.errors.map((e) => {
        toast.error(e);
      });
    }
  };

  return <GameForm handleLogic={handleEdit} gameData={game} buttonValue={"Edit"}/>;
};

export default EditGameForm;
