import React, { useEffect, useState } from "react";
import useAuth from "../../hooks/useAuth";
import { Link, useLocation, useNavigate, useParams } from "react-router-dom";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import StarRating from "../../components/game/StarRating/StarRating";
import { toast } from "react-toastify";
import AddToCollectionForm from "components/game/AddToCollectionForm/AddToCollectionForm";
import { Button, Modal } from "react-bootstrap";
import AddGameRoundForm from "components/gameRound/AddGameRoundForm/AddGameRoundForm";
import EditGameForm from "components/game/EditGameForm/EditGameForm";
import { useTranslation } from "react-i18next";

const GameDetails = () => {
  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();

  const [game, setGame] = useState({});
  const [rating, setRating] = useState(-1);
  const [availableCollections, setAvailableCollections] = useState([]);
  const [show, setShow] = useState(false);
  const [showEdit, setShowEdit] = useState(false);
  const [showAddRound, setShowAddRound] = useState(false);

  const handleEditClose = () => setShowEdit(false);
  const handleEditShow = () => setShowEdit(true);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const handleAddRoundClose = () => setShowAddRound(false);
  const handleAddRoundShow = () => setShowAddRound(true);

  const navigate = useNavigate();
  const location = useLocation();
  const { t } = useTranslation();

  const { gameId } = useParams();

  useEffect(() => {
    if (rating === -1) {
      getGame();
    }
  }, [rating]);

  const getGame = async () => {
    try {
      const response = await axiosPrivate.get(`/game/details/${gameId}`);
      var data = response.data.result;
      setGame(data.game);
      setRating(data.rating);
      setAvailableCollections(data.availableCollections);
    } catch (err) {
      if (err.name === "CanceledError") {
        return;
      }

      console.error(err);
      navigate("/login", { state: { from: location }, replace: true });
    }
  };

  const resetRating = () => {
    setRating(-1);
  };

  const handleSubmit = () => {
    handleClose();
    resetRating();
  };

  const handleDelete = async (id) => {
    if (window.confirm(t("game.deleteConfirm"))) {
      const response = await axiosPrivate.delete(`/game/${id}`);
      console.log(response);
      if (response.data.succeeded) {
        console.log(response.data);
        toast.success(t("game.deletedMes"));
        navigate("/games", { state: { from: location }, replace: true });
      }
    }
  };

  return (
    <>
      <div>
        <h2 className="text-center">{game.name}</h2>
        <div className="card h-100 w-75 mx-auto">
          <img
            className="card-img-top"
            src={game.photoPath}
            alt={t("game.game")}
          />
          <div className="card-body my-auto">
            <div className="d-flex">
              <div>
                <h4 className="card-title">
                  {game.nameRu}{" "}
                  <span className="text-black-50">({game.minAge}+)</span>
                </h4>
                <p className="card-text">
                  {t("game.from")} {game.minPlayers} {t("game.to")}{" "}
                  {game.maxPlayers} {t("game.participants")}
                  <br />
                  {t("game.time")}: {game.minPlayTime} - {game.maxPlayTime}{" "}
                  {t("game.minutes")}
                  <br />
                </p>
              </div>
              <div className="ms-auto mb-auto text-end">
                {rating === null ? (
                  <StarRating
                    isEditable={true}
                    resetRating={resetRating}
                    game={game}
                  />
                ) : (
                  <>
                    <StarRating
                      isEditable={false}
                      value={rating}
                      game={game}
                      resetRating={resetRating}
                    />
                  </>
                )}
              </div>
            </div>

            {auth?.id && (
              <>
                <div className="my-3">
                  {availableCollections.length === 0 ? (
                    <p>{t("game.gameInCollection")}</p>
                  ) : (
                    <button
                      type="button"
                      className="btn btn-warning btn-sm me-2"
                      onClick={handleShow}
                    >
                      {t("game.addToCollection")}
                    </button>
                  )}
                  <button
                    className="btn btn-warning btn-sm"
                    onClick={handleAddRoundShow}
                  >
                    {t("game.addRoundResult")}
                  </button>
                </div>
                <div className="my-3">
                  <Link
                    className="btn-sm me-2 text-black-50"
                    to={`/gameRounds/${gameId}/${game.name}`}
                  >
                    <FontAwesomeIcon
                      icon="fa-solid fa-square-poll-vertical"
                      size="2xl"
                      style={{ color: "#fdce3f" }}
                    />{" "}
                    {t("game.results")}
                  </Link>
                </div>
              </>
            )}
            <div>
              {auth?.roles?.includes("admin") && (
                <>
                  <button
                    className="btn btn-dark btn-sm ms-2"
                    onClick={handleEditShow}
                  >
                    {t("forms.edit")}
                  </button>
                  <button
                    className="btn btn-dark btn-sm ms-2"
                    onClick={() => handleDelete(gameId)}
                  >
                    {t("forms.delete")}
                  </button>
                </>
              )}
            </div>
          </div>
          <div className="card-footer text-body-secondary">
            {t("game.releasedIn")} {game.yearOfRelease}
            <br />
          </div>
        </div>
      </div>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>{t("game.addToCollection")}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <AddToCollectionForm
            handleSubmit={handleSubmit}
            availableCollections={availableCollections}
            gameId={gameId}
            game={game}
          />
        </Modal.Body>
      </Modal>

      <Modal show={showEdit} onHide={handleEditClose}>
        <Modal.Header closeButton>
          <Modal.Title>
            {t("forms.edit")} {t("game.game")}
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <EditGameForm
            handleClose={handleEditClose}
            gameId={gameId}
            game={game}
            updateGame={getGame}
          />
        </Modal.Body>
      </Modal>

      <Modal show={showAddRound} onHide={handleAddRoundClose} scrollable={true}>
        <Modal.Header closeButton>
          <Modal.Title>{t("game.addRoundResult")}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <AddGameRoundForm gameProp={game} handleClose={handleAddRoundClose} />
        </Modal.Body>
      </Modal>
    </>
  );
};

export default GameDetails;
