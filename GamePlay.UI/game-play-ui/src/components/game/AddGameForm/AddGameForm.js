import useAxiosPrivate from "hooks/useAxiosPrivate";
import React, { useState } from "react";
import { toast } from "react-toastify";
import GameForm from "../GameForm/GameForm";

const AddGameForm = ({ handleClose, updateData }) => {
  const axiosPrivate = useAxiosPrivate();
  

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
      toast.success("Game has been added");
    } else {
      console.log(response);
      response.data.errors.map((e) => {
        toast.error(e);
      });
    }
  };

  return <GameForm handleLogic={handleAdd} buttonValue={"Add"}/>;
};

export default AddGameForm;
