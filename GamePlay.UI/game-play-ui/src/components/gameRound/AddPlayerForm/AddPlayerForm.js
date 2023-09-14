import React, { useState } from "react";
import Select from "react-select";
import { toast } from "react-toastify";

const AddPlayerForm = ({ users, handleClose }) => {
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

  const handlePlayerAdd = (e) => {
    if (player.name === "") {
      toast.error("Enter player's name first!");
      return;
    }
    e.preventDefault();
    handleClose(player);
  };

  return (
    <div>
      <div id="newPlayer" className="my-2 text-start">
        <input
          id="player-name"
          name="name"
          className="form-control mb-2"
          placeholder="Enter name"
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
            Is Registered?
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
              placeholder="Select user"
              value={selectedOption}
              onChange={onSelect}
              isSearchable={true}
              className="mb-5"
            />
          </div>
        )}
        <input
          type="button"
          value="Add Player"
          className="btn btn-light d-block w-100 my-2"
          id="add-player-button"
          onClick={handlePlayerAdd}
        />
      </div>
    </div>
  );
};

export default AddPlayerForm;
