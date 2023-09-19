import React, { useState } from "react";
import { useTranslation } from "react-i18next";

const GameForm = ({ handleLogic, gameData, buttonValue }) => {
  const [form, setForm] = useState(gameData === undefined ? {
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
  } : gameData);
  const [file, setFile] = useState(null);
  const { t } = useTranslation();

  const saveFile = (e) => {
    console.log(e.target?.files[0]);
    setFile(e.target?.files[0]);
  };

  const onChange = (e) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const formData = new FormData();
    formData.append("gameModelJson", JSON.stringify(form));
    formData.append("gameImage", file);
    handleLogic(formData);
  };

  return (
    <form onSubmit={(e) => handleSubmit(e)}>
      <div className="form-group">
        <input className="form-control" type="file" onChange={saveFile} />
      </div>
      <div className="form-group">
        <label htmlFor="name" className="control-label">
        {t("forms.name")}
        </label>
        <input
          name="name"
          id="name"
          value={form.name}
          className="form-control"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="nameRu" className="control-label">
        {t("game.nameRu")}
        </label>
        <input
          name="nameRu"
          id="nameRu"
          value={form.nameRu}
          className="form-control"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="nameEn" className="control-label">
        {t("game.nameEn")}
        </label>
        <input
          name="nameEn"
          id="nameEn"
          value={form.nameEn}
          className="form-control"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="minPlayers" className="control-label">
        {t("game.minPlayers")}
        </label>
        <input
          name="minPlayers"
          id="minPlayers"
          value={form.minPlayers}
          className="form-control"
          type="number"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="maxPlayers" className="control-label">
        {t("game.maxPlayers")}
        </label>
        <input
          name="maxPlayers"
          id="maxPlayers"
          value={form.maxPlayers}
          className="form-control"
          type="number"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="minAge" className="control-label">
        {t("game.minAge")}
        </label>
        <input
          name="minAge"
          id="minAge"
          value={form.minAge}
          className="form-control"
          type="number"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="minPlayTime" className="control-label">
        {t("game.minPlayTime")}
        </label>
        <input
          name="minPlayTime"
          id="minPlayTime"
          value={form.minPlayTime}
          className="form-control"
          type="number"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="maxPlayTime" className="control-label">
        {t("game.maxPlayTime")}
        </label>
        <input
          name="maxPlayTime"
          id="maxPlayTime"
          value={form.maxPlayTime}
          className="form-control"
          type="number"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="yearOfRelease" className="control-label">
        {t("game.yearOfRelease")}
        </label>
        <input
          id="yearOfRelease"
          name="yearOfRelease"
          value={form.yearOfRelease}
          className="form-control"
          type="number"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="description" className="control-label">
        {t("game.description")}
        </label>
        <input
          name="description"
          id="description"
          value={form.description}
          className="form-control"
          onChange={onChange}
          required
        />
      </div>
      <div className="form-group">
        <input type="submit" value={buttonValue} className="btn btn-primary" />
      </div>
    </form>
  );
};

export default GameForm;
