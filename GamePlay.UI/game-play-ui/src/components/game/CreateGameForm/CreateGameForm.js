import useAxiosPrivate from "hooks/useAxiosPrivate";
import React, { useState } from "react";
import { json } from "react-router-dom";
import { toast } from "react-toastify";

const CreateGameForm = ({ handleClose, updateGames }) => {
  const axiosPrivate = useAxiosPrivate();
  const [form, setForm] = useState({
    name: "",
    nameRu: "",
    nameEn: "",
    minPlayers: 0,
    maxPlayers: 0,
    minAge: 0,
    minPlayTime: 0,
    maxPlayTime: 0,
    yearOfRelease: 0,
    description: "",
  });
  const [file, setFile] = useState(null);

  const saveFile = (e) => {
    console.log(e.target?.files[0]);
    setFile(e.target?.files[0]);
  };

  const onChange = (e) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  };

  const handleAdd = async (e) => {
    e.preventDefault();
    const formData = new FormData();
    console.log(form);
    console.log(file);
    formData.append("gameModelJson", JSON.stringify(form));
    formData.append("gameImage", file);

    const response = await axiosPrivate.post("game", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
    if (response.data.succeeded){
      console.log(response);
      handleClose();
      updateGames();
      toast.success("Game has been added");      
    }
    else{
      console.log(response)
      response.data.errors.map((e) => {
        toast.error(e); 
      })
    }
  };

  return (
    <form onSubmit={handleAdd}>
      <div className="form-group">
        <input className="form-control" type="file" onChange={saveFile} />
      </div>
      <div className="form-group">
        <label htmlFor="name" className="control-label">
          Name
        </label>
        <input
          name="name"
          id="name"
          className="form-control"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="nameRu" className="control-label">
          Name in Russian
        </label>
        <input
          name="nameRu"
          id="nameRu"
          className="form-control"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="nameEn" className="control-label">
          Name in English
        </label>
        <input
          name="nameEn"
          id="nameEn"
          className="form-control"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="minPlayers" className="control-label">
          Minimum number of players
        </label>
        <input
          name="minPlayers"
          id="minPlayers"
          className="form-control"
          type="number"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="maxPlayers" className="control-label">
          Maximum number of players
        </label>
        <input
          name="maxPlayers"
          id="maxPlayers"
          className="form-control"
          type="number"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="minAge" className="control-label">
          Minimum age
        </label>
        <input
          name="minAge"
          id="minAge"
          className="form-control"
          type="number"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="minPlayTime" className="control-label">
          Minimum play time
        </label>
        <input
          name="minPlayTime"
          id="minPlayTime"
          className="form-control"
          type="number"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="maxPlayTime" className="control-label">
          Maximum play time
        </label>
        <input
          name="maxPlayTime"
          id="maxPlayTime"
          className="form-control"
          type="number"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="yearOfRelease" className="control-label">
          Year of release
        </label>
        <input
          id="yearOfRelease"
          name="yearOfRelease"
          className="form-control"
          type="number"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="description" className="control-label">
          Description
        </label>
        <input
          name="description"
          id="description"
          className="form-control"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <input type="submit" value="Create" className="btn btn-primary" />
      </div>
    </form>
  );
};

export default CreateGameForm;
