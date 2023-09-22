import useAxiosPrivate from "hooks/useAxiosPrivate";
import React, { useState } from "react";
import { useTranslation } from "react-i18next";
import Select from "react-select";
import { toast } from "react-toastify";

const AddPlayerForm = ({ users, handleClose }) => {
  const axiosPrivate = useAxiosPrivate();
  const { t } = useTranslation();

  const [player, setPlayer] = useState({
    name: "",
    isRegistered: false,
    userId: null,
  });
  const [options, setOptions] = useState(
    users.map((user) => ({ value: user, label: user.userName }))
  );
  const [selectedOption, setSelectedOption] = useState();

  const onSelect = (selectedOption) => {
    setPlayer((prev) => ({ ...prev, ["userId"]: selectedOption.value.id }));
    setSelectedOption(selectedOption);
  };

  const onChangeForm = (e) => {
    const { name, value } = e.target;
    setPlayer((prev) => ({ ...prev, [name]: value }));
  };

  const onChangeCheckbox = (e) => {
    const { name, checked } = e.target;
    setPlayer((prev) => ({ ...prev, [name]: checked }));
  };

  const handlePlayerAdd = async (e) => {
    if (player.name === "") {
      toast.error(t("player.nameValidation"));
      return;
    }
    e.preventDefault();

    const response = await axiosPrivate.post(`player/create`, player);
    if (response.data.succeeded) {
      player.id = response.data.result.id;
      handleClose(player);
      toast.success(t("player.addedMes"));
    } else {
      console.log(response);
      response.data.errors.map((e) => {
        toast.error(e);
      });
    }
  };

  return (
    <div>
      <div id="newPlayer" className="my-2 text-start">
        <input
          id="player-name"
          name="name"
          className="form-control mb-2"
          placeholder={t("forms.enter") + " " + t("player.name")}
          list="players"
          value={player.name}
          onChange={onChangeForm}
        />
        <div className="form-check d-flex">
          <input
            id="player-is-registered"
            className="form-check-input ps-0"
            type="checkbox"
            name="isRegistered"
            checked={player.isRegistered}
            onChange={onChangeCheckbox}
          />
          <label className="form-check-label" htmlFor="player-is-registered">
            {t("player.isRegistered")}
          </label>
        </div>
        {player.isRegistered && (
          <div
            className="form-group"
            id="user-select-div"
            style={{
              display: player.isRegistered === true ? "block" : "none",
            }}
          >
            <Select
              options={options}
              placeholder={t("forms.choose") + " " + t("user.what")}
              value={selectedOption}
              onChange={onSelect}
              isSearchable={true}
              className="mb-5"
            />
          </div>
        )}
        <input
          type="button"
          value={t("forms.add")}
          className="btn btn-light d-block w-100 my-2"
          id="add-player-button"
          onClick={handlePlayerAdd}
        />
      </div>
    </div>
  );
};

export default AddPlayerForm;
