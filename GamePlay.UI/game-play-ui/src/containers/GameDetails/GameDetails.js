import React, { useEffect, useState } from "react";
import useAuth from "../../hooks/useAuth";
import { Link, useLocation, useNavigate, useParams } from "react-router-dom";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import StarRating from "../../components/game/StarRating/StarRating";
import { toast } from "react-toastify";
import AddToCollectionForm from "components/game/AddToCollectionForm/AddToCollectionForm";
import { Button, Modal } from "react-bootstrap";

const GameDetails = () => {
  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();

  const [game, setGame] = useState({});
  const [rating, setRating] = useState(-1);
  const [availableCollections, setAvailableCollections] = useState([]);
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const navigate = useNavigate();
  const location = useLocation();

  const { gameId } = useParams();

  useEffect(() => {
    let isMounted = true;
    const controller = new AbortController();

    const getGame = async () => {
      try {
        const response = await axiosPrivate.get(`/game/details/${gameId}`, {
          signal: controller.signal,
        });
        if (isMounted) {
          var data = response.data.result;
          setGame(data.game);
          setRating(data.rating);
          setAvailableCollections(data.availableCollections);
        }
      } catch (err) {
        if (err.name === "CanceledError") {
          return;
        }

        console.error(err);
        navigate("/login", { state: { from: location }, replace: true });
      }
    };

    if (rating === -1) {
      getGame();
    }

    return () => {
      isMounted = false;
      controller.abort();
    };
  }, [rating]);

  const resetRating = () => {
    console.log("RESET RATING");
    setRating(-1);
  };

  const handleSubmit = () => {
    handleClose();
    resetRating();
  };

  return (
    <>
      <div>
        <h2 className="text-center">{game.name}</h2>
        <div className="card h-100 w-75 mx-auto">
          <img className="card-img-top" src={game.photoPath} alt="Game image" />
          <div className="card-body my-auto">
            <div className="d-flex">
              <div>
                <h4 className="card-title">
                  {game.nameRu} / {game.nameEn}{" "}
                  <span className="text-black-50">({game.minAge}+)</span>
                </h4>
                <p className="card-text">
                  From {game.minPlayers} to {game.maxPlayers} participants
                  <br />
                  Time: {game.minPlayTime} - {game.maxPlayTime} minutes
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
              <div className="my-3">
                {availableCollections.length === 0 ? (
                  <p>The game is already in all your collections.</p>
                ) : (
                  <button
                    type="button"
                    className="btn btn-warning"
                    onClick={handleShow}
                  >
                    Add to collection
                  </button>
                )}
              </div>
            )}
            <div className="my-3">
              {auth?.id && (
                <Link className="btn-sm me-2 text-black-50" to={`/gameRounds/${gameId}/${game.name}`}>
                  <FontAwesomeIcon
                    icon="fa-solid fa-square-poll-vertical"
                    size="2xl"
                    style={{ color: "#fdce3f" }}
                  />{" "}
                  Results
                </Link>
              )}
            </div>
            <div>
              {auth?.roles?.includes("admin") && (
                <>
                  <a className="btn btn-dark btn-sm">Add a Round Result</a>
                  <a className="btn btn-dark btn-sm ms-2">Edit</a>
                  <a className="btn btn-dark btn-sm ms-2">Delete</a>
                </>
              )}
            </div>
          </div>
          <div className="card-footer text-body-secondary">
            Released in {game.yearOfRelease}
            <br />
          </div>
        </div>
      </div>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Add game to collection</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <AddToCollectionForm
            handleSubmit={handleSubmit}
            availableCollections={availableCollections}
            gameId={gameId}
          />
        </Modal.Body>
      </Modal>
    </>
  );
};

export default GameDetails;
