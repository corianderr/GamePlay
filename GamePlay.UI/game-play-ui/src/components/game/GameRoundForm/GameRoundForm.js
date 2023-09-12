import moment from "moment";
import React, { useEffect, useRef, useState } from "react";
import Select from "react-select";
import { toast } from "react-toastify";

const GameRoundForm = ({
  game,
  setGame,
  viewModel,
  submitResult,
  buttonName,
  games,
}) => {
  const inputRef = useRef(null);
  const [players, setPlayers] = useState([]);
  const [form, setForm] = useState({
    gameId: game?.id,
    game: null,
    date: moment(new Date()).format("YYYY-MM-DD"),
    place: "",
  });
  const [player, setPlayer] = useState({
    gameRoundId: null,
    name: "",
    score: 0,
    role: "",
    isWinner: false,
    isRegistered: false,
    userId: null,
  });
  const [options, setOptions] = useState([]);
  const [selectedOption, setSelectedOption] = useState([]);

  useEffect(() => {
    if (viewModel?.gameRound?.game === undefined && game === undefined) {
      inputRef.current.focus();
    }
  }, []);

  useEffect(() => {
    if (options?.length === 0) {
      games?.map((game) => {
        options.push({ value: game.id, label: game.name });
      });
    }
  }, [games]);

  useEffect(() => {
    if (viewModel?.gameRound.id === undefined) {
      setPlayers([]);
      setForm({
        gameId: game?.id,
        game: null,
        date: moment(new Date()).format("YYYY-MM-DD"),
        place: "",
      });
    } else {
      setPlayers(viewModel?.gameRound.players);
      setForm(viewModel?.gameRound);
    }
    if (viewModel?.gameRound?.game !== undefined) {
      setSelectedOption({
        value: viewModel?.gameRound?.game.id,
        label: viewModel?.gameRound?.game.name,
      });
    }
  }, [viewModel]);

  const onChangeForm = (e) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  };

  const onChangePlayer = (e) => {
    const { name, value } = e.target;
    setPlayer((prev) => ({ ...prev, [name]: value }));
  };

  const onChangePlayerCheckbox = (e) => {
    const { name, checked } = e.target;
    setPlayer((prev) => ({ ...prev, [name]: checked }));
  };

  const handlePlayerAdd = (e) => {
    if (player.name === "") {
      toast.error("Enter player's name first!");
      return;
    }
    e.preventDefault();
    setPlayers((players) => [...players, player]);
    cleanPlayer();
    console.log(players);
  };

  const cleanPlayer = () => {
    setPlayer({
      gameRoundId: null,
      name: "",
      score: 0,
      role: "",
      isWinner: false,
      isRegistered: false,
      userId: null,
    });
  };

  const handleSubmitResult = (e) => {
    e.preventDefault();
    form.players = players;
    submitResult(form);
  };

  const handleSelect = (data) => {
    setGame((prev) => ({ ...prev, ["id"]: data.value }));
    setSelectedOption(data);
  };

  return (
    <div className="text-center">
      <div className="row">
        <div className="col-md-10 mx-auto">
          <form id="gameForm" onSubmit={(e) => handleSubmitResult(e)}>
            {viewModel?.gameRound?.game === undefined && (
              <h4 className="control-label text-primary mb-3" htmlFor="date">
                Choose the game to start ↓
              </h4>
            )}
            <Select
              options={options}
              placeholder="Select game"
              value={selectedOption}
              onChange={handleSelect}
              isSearchable={true}
              isDisabled={buttonName === "Edit"}
              className="mb-5"
              ref={inputRef}
            />

            <div
              style={{
                pointerEvents: viewModel?.gameRound?.game === undefined ? "none" : "auto",
                color: viewModel?.gameRound?.game === undefined && "gray",
              }}
              disabled={true}
            >
              <div className="form-group">
                <label className="control-label" htmlFor="date">
                  Date
                </label>
                <input
                  className="form-control"
                  type="date"
                  value={form.date.substring(0, 10)}
                  name="date"
                  id="date"
                  onChange={onChangeForm}
                  required
                />
              </div>
              <div className="form-group">
                <label className="control-label" htmlFor="place">
                  Place
                </label>
                <input
                  className="form-control"
                  list="places"
                  value={form.place}
                  name="place"
                  id="place"
                  onChange={onChangeForm}
                  required
                />
                <datalist id="places">
                  {viewModel?.previousPlaces.map((item) => (
                    <option key={item} value={item} />
                  ))}
                </datalist>
              </div>
              <div>
                <h4 className="d-inline-block mr-3">
                  Players ({viewModel?.gameRound.game.minPlayers} -{" "}
                  {viewModel?.gameRound.game.maxPlayers})
                </h4>
                <hr />
                <div id="newPlayer" className="my-2">
                  <input
                    id="player-name"
                    name="name"
                    className="form-control mb-2"
                    placeholder="Enter name"
                    list="players"
                    value={player.name}
                    onChange={onChangePlayer}
                  />
                  <datalist id="players">
                    {viewModel?.previousOpponents.map((item) => (
                      <option key={item} value={item} />
                    ))}
                  </datalist>
                  <input
                    id="player-role"
                    name="role"
                    className="form-control mb-2"
                    placeholder="Enter role"
                    value={player.role}
                    onChange={onChangePlayer}
                  />
                  <input
                    id="player-score"
                    name="score"
                    className="form-control mb-2"
                    placeholder="Enter score"
                    type="number"
                    value={player.score}
                    onChange={onChangePlayer}
                  />
                  <div className="form-check d-flex">
                    <input
                      id="player-is-winner"
                      name="isWinner"
                      className="form-check-input ps-0"
                      type="checkbox"
                      checked={player.isWinner}
                      onChange={onChangePlayerCheckbox}
                    />
                    <label
                      className="form-check-label"
                      htmlFor="player-is-winner"
                    >
                      Is Winner?
                    </label>
                  </div>
                  <div className="form-check d-flex">
                    <input
                      id="player-is-registered"
                      className="form-check-input ps-0"
                      type="checkbox"
                      name="isRegistered"
                      checked={player.isRegistered}
                      onChange={onChangePlayerCheckbox}
                    />
                    <label
                      className="form-check-label"
                      htmlFor="player-is-registered"
                    >
                      Is Registered?
                    </label>
                  </div>
                  {player.isRegistered && (
                    <div
                      className="form-group"
                      id="user-select-div"
                      style={{
                        display:
                          player.isRegistered === true ? "block" : "none",
                      }}
                    >
                      <select
                        className="form-control"
                        id="player-user-id"
                        name="userId"
                        onChange={onChangePlayer}
                      >
                        <option value="" selected>
                          Select User
                        </option>
                        {viewModel?.users.map((item) => (
                          <option key={item.id} value={item.id}>
                            {item.userName}
                          </option>
                        ))}
                      </select>
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
                <table className="table table-bordered mt-5" id="player-table">
                  <thead>
                    <tr>
                      <th scope="col">Name</th>
                      <th scope="col">Role</th>
                      <th scope="col">Score</th>
                      <th scope="col">Is Winner?</th>
                      <th scope="col">Is Registered?</th>
                      <th scope="col">User Id</th>
                    </tr>
                  </thead>
                  <tbody style={{ fontWeight: "normal" }}>
                    {players.map((item, i) => (
                      <tr key={i}>
                        <td>{item.name}</td>
                        <td>{item.role}</td>
                        <td>{item.score}</td>
                        <td>
                          <input
                            className="form-check-input"
                            type="checkbox"
                            checked={item.isWinner}
                            disabled
                          />
                        </td>
                        <td>
                          <input
                            className="form-check-input"
                            type="checkbox"
                            checked={item.isRegistered}
                            disabled
                          />
                        </td>
                        <td>{item.isRegistered ? item.userId : ""}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
              <div className="form-group">
                <input
                  type="submit"
                  value={`${buttonName} Round Result`}
                  className="btn btn-primary"
                />
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default GameRoundForm;
