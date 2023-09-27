import { Link, useLocation, useNavigate } from "react-router-dom";
import "./GameList.css";
import { useTranslation } from "react-i18next";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import useAxiosPrivate from "hooks/useAxiosPrivate";
import { toast } from "react-toastify";
import { useEffect, useState } from "react";
import { Pagination } from "@mui/material";
import useAuth from "hooks/useAuth";
import { Modal } from "react-bootstrap";
import EditGameForm from "../EditGameForm/EditGameForm";

const GameList = ({ header, games, collectionId, refreshGames }) => {
  const { t } = useTranslation();
  const { auth } = useAuth();

  const navigate = useNavigate();
  const location = useLocation();
  const axiosPrivate = useAxiosPrivate();

  const [showEdit, setShowEdit] = useState(false);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);
  const [subset, setSubset] = useState([]);

  const [editGame, setEditGame] = useState([]);

  const itemsPerPage = 9;

  const handleEditClose = () => setShowEdit(false);
  const handleEditShow = (e, game) => {
    e.stopPropagation();
    setShowEdit(true);
    setEditGame(game);
  };

  const handlePageChange = (event, value) => {
    setCurrentPage(value);
  };

  useEffect(() => {
    setTotalPages(Math.ceil(games?.length / itemsPerPage));
    updateSubset();
  }, [games]);

  useEffect(() => {
    updateSubset();
  }, [currentPage]);

  const updateSubset = () => {
    const startIndex = (currentPage - 1) * itemsPerPage;
    const endIndex = startIndex + itemsPerPage;
    setSubset(games?.slice(startIndex, endIndex));
  };

  const redirectToGameDetails = (gameId) => {
    navigate(`/gameDetails/${gameId}`);
  };

  const deleteFromCollectoin = async (gameId) => {
    if (
      !window.confirm(
        `Are you sure you want to remove this game from ${header} collection?`
      )
    ) {
      return;
    }

    const response = await axiosPrivate.post(
      `collection/deleteGame?id=${gameId}&collectionId=${collectionId}`
    );
    if (response.data.succeeded) {
      toast.success("Game has been removed");
      refreshGames();
    } else {
      toast.error("Error...");
    }
  };

  const handleDelete = async (e, id) => {
    e.stopPropagation();
    if (window.confirm(t("game.deleteConfirm"))) {
      const response = await axiosPrivate.delete(`/game/${id}`);
      console.log(response);
      if (response.data.succeeded) {
        console.log(response.data);
        refreshGames();
        toast.success(t("game.deletedMes"));
        navigate("/games", { state: { from: location }, replace: true });
      }
    }
  };

  return (
    <>
      {games?.length === 0 ? (
        <h5 className="mt-3">{t("game.noGames")}</h5>
      ) : (
        <>
          <div className="container mt-3">
            <h2 className="text-center">{header}</h2>
            <div className="row mt-3">
              {subset.map((game, i) => (
                <div className="col-lg-4 col-sm-6 mb-3" key={i}>
                  <div className="card h-100">
                    <div
                      className="p-3 d-flex flex-column h-100"
                      onClick={() => redirectToGameDetails(game.id)}
                      style={{ cursor: "pointer" }}
                    >
                      <div className="d-flex flex-row flex-wrap mb-3">
                        <img
                          src={game.photoPath}
                          width="70"
                          className="game-cover"
                          alt={game.name}
                        />
                        <div className="d-flex flex-column ms-2">
                          <span>{game.name}</span>
                          <span className="text-black-50">
                            {game.nameRu} ({game.minAge}+)
                          </span>
                          {game.averageRating !== undefined && (
                            <span className="ratings">
                              {game.averageRating.toFixed(2)}{" "}
                              <i className="fa fa-star"></i>
                            </span>
                          )}
                        </div>
                      </div>
                      <p style={{ fontWeight: "600" }}>
                        {t("game.from")} {game.minPlayers} {t("game.to")}{" "}
                        {game.maxPlayers} {t("game.participants")} <br />
                        {t("game.time")}: {game.minPlayTime} -{" "}
                        {game.maxPlayTime} {t("game.minutes")}
                      </p>
                      <div className="d-flex justify-content-end install mt-auto align-items-center">
                        <span>
                          {t("game.releasedIn")} {game.yearOfRelease}
                        </span>
                        <div>
                          {auth?.roles?.includes("admin") && (
                            <>
                              <button
                                className="ms-2 btn btn-sm"
                                onClick={(event) => handleEditShow(event, game)}
                              >
                                <FontAwesomeIcon icon="fa-solid fa-pen-to-square" />
                              </button>
                              <button
                                className="btn btn-sm"
                                onClick={(event) =>
                                  handleDelete(event, game.id)
                                }
                              >
                                <FontAwesomeIcon icon="fa-solid fa-trash" />
                              </button>
                            </>
                          )}
                        </div>
                      </div>
                    </div>
                    {collectionId !== undefined && (
                      <div
                        className="bg-danger text-center"
                        style={{ borderRadius: "0 0 5px 5px" }}
                      >
                        <button
                          className="btn"
                          onClick={() => deleteFromCollectoin(game.id)}
                        >
                          <FontAwesomeIcon
                            className="text-light"
                            icon="fa-solid fa-trash"
                          />
                        </button>
                      </div>
                    )}
                  </div>
                </div>
              ))}
            </div>
          </div>
        </>
      )}
      {totalPages > 1 && (
        <Pagination
          count={totalPages}
          page={currentPage}
          onChange={handlePageChange}
        />
      )}
      <Modal show={showEdit} onHide={handleEditClose}>
        <Modal.Header closeButton>
          <Modal.Title>
            {t("forms.edit")} {t("game.what")}
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <EditGameForm
            handleClose={handleEditClose}
            gameId={editGame.id}
            game={editGame}
            updateGame={refreshGames}
          />
        </Modal.Body>
      </Modal>
    </>
  );
};
export default GameList;
