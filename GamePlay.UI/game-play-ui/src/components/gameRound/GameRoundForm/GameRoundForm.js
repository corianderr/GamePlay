import moment from "moment";
import React, { useEffect, useRef, useState } from "react";
import Select from "react-select";
import AddPlayerForm from "../AddPlayerForm/AddPlayerForm";
import { Modal } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useTranslation } from "react-i18next";

const GameRoundForm = ({
  game,
  setGame,
  viewModel,
  submitResult,
  buttonName,
  games,
  getData
}) => {
  const { t } = useTranslation();
  const inputRef = useRef(null);

  const [players, setPlayers] = useState([]);
  const [form, setForm] = useState({
    gameId: game?.id,
    game: null,
    date: moment(new Date()).format("YYYY-MM-DD"),
    place: "",
  });
  const [options, setOptions] = useState([]);
  const [selectedOption, setSelectedOption] = useState({});
  const [playerOptions, setPlayerOptions] = useState([]);
  const [selectedPlayers, setSelectedPlayers] = useState([]);

  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  useEffect(() => {
    if (viewModel?.gameRound?.game === undefined && game === undefined) {
      inputRef.current.focus();
    }
    if (playerOptions?.length === 0) {
      setPlayerOptions(
        viewModel?.previousOpponents?.map((user) => ({
          value: user,
          label: user.name,
        })) || []
      );
    }
  }, [viewModel]);

  useEffect(() => {
    if (options?.length === 0) {
      setOptions(
        games?.map((game) => ({ value: game.id, label: game.name })) || []
      );
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

  const refreshPlayers = (player) => {
    handleClose();
    setPlayerOptions((players) => [
      ...players,
      { value: player, label: player.name },
    ]);
  };

  const handleSubmitResult = (e) => {
    e.preventDefault();
    form.players = players;
    form.game = viewModel?.gameRound.game;
    submitResult(form);
  };

  const handleSelect = (data) => {
    setGame((prev) => ({ ...prev, ["id"]: data.value }));
    setSelectedOption(data);
  };

  const handleSelectPlayers = (selectedOption) => {
    setSelectedPlayers(selectedOption);
    setPlayers(
      selectedOption
        .map((player) => player.value)
        .map((p) => ({
          ...p,
          playerId: p.id,
          score: 0,
          role: "",
          isWinner: false,
        }))
    );
    console.log(players);
  };

  const updatePlayers = (name, value, index) => {
    setPlayers((prevPlayers) => {
      const updatedPlayers = [...prevPlayers];
      updatedPlayers[index] = {
        ...updatedPlayers[index],
        [name]: value,
      };
      return updatedPlayers;
    });
  };

  const handlePlayerInputChange = (e, index) => {
    const { name, value } = e.target;
    updatePlayers(name, value, index);
  };

  const handlePlayerCheckboxChange = (e, index) => {
    const { name, checked } = e.target;
    updatePlayers(name, checked, index);
  };

  return (
    <>
      {show && <div className="shaded-modal"></div>}
      <div className="text-center">
        <div className="row">
          <div className="col-md-10 mx-auto">
            <form id="gameForm" onSubmit={(e) => handleSubmitResult(e)}>
              {viewModel?.gameRound?.game === undefined && (
                <h4 className="control-label text-primary mb-3" htmlFor="date">
                  {t("roundResult.chooseGame")} ↓
                </h4>
              )}
              <Select
                options={options}
                placeholder={t("forms.choose") + t("game.what")}
                value={selectedOption}
                onChange={handleSelect}
                isSearchable={true}
                isDisabled={viewModel?.gameRound.id !== undefined}
                className="mb-5"
                ref={inputRef}
              />

              <div
                style={{
                  pointerEvents:
                    viewModel?.gameRound?.game === undefined ? "none" : "auto",
                  color: viewModel?.gameRound?.game === undefined && "gray",
                }}
                disabled={true}
              >
                <div className="form-group">
                  <label className="control-label" htmlFor="date">
                  {t("roundResult.date")}
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
                  {t("roundResult.place")}
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
                  {t("roundResult.players")} ({viewModel?.gameRound.game.minPlayers} -{" "}
                    {viewModel?.gameRound.game.maxPlayers})
                  </h4>
                  <hr />
                  <div className="d-flex mb-5">
                    <Select
                      options={playerOptions}
                      placeholder={t("roundResult.selectPlayers")}
                      value={selectedPlayers}
                      onChange={handleSelectPlayers}
                      isSearchable={true}
                      className="flex-grow-1"
                      isMulti
                    />
                    <span
                      onClick={handleShow}
                      style={{ cursor: "pointer" }}
                      className="my-auto"
                    >
                      <FontAwesomeIcon
                        icon="fa-solid fa-square-plus"
                        size="2xl"
                        className="ms-2 opacity-75"
                      />
                    </span>
                  </div>

                  <table
                    className="table table-bordered mt-5"
                    id="player-table"
                  >
                    <thead>
                      <tr>
                        <th scope="col">{t("player.name")}</th>
                        <th scope="col">{t("player.role")}</th>
                        <th scope="col">{t("player.score")}</th>
                        <th scope="col">{t("player.isWinner")}?</th>
                        <th scope="col">{t("player.isRegistered")}?</th>
                        <th scope="col">{t("player.userId")}</th>
                      </tr>
                    </thead>
                    <tbody style={{ fontWeight: "normal" }}>
                      {players.map((item, i) => (
                        <tr key={i}>
                          <td>{item.name}</td>
                          <td>
                            <input
                              className="form-control"
                              name="role"
                              value={item.role}
                              onChange={(e) => handlePlayerInputChange(e, i)}
                            />
                          </td>
                          <td>
                            <input
                              className="form-control"
                              type="number"
                              name="score"
                              value={item.score}
                              onChange={(e) => handlePlayerInputChange(e, i)}
                            />
                          </td>
                          <td>
                            <input
                              className="form-check-input"
                              type="checkbox"
                              name="isWinner"
                              checked={item.isWinner}
                              onChange={(e) => handlePlayerCheckboxChange(e, i)}
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
                          <td>{item.isRegistered ? item.id.substring(0, 8) : "–"}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
                <div className="form-group">
                  <input
                    type="submit"
                    value={`${buttonName}`}
                    className="btn btn-primary"
                  />
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>{t("forms.add")} {t("player.what")}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <AddPlayerForm
            users={viewModel?.users}
            handleClose={refreshPlayers}
          />
        </Modal.Body>
      </Modal>
    </>
  );
};

export default GameRoundForm;
