import React, { useEffect, useState } from "react";
import useAuth from "../../hooks/useAuth";
import { Link, useLocation, useNavigate, useParams } from "react-router-dom";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import StarRating from "./StarRating";

const GameDetails = () => {
  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();

  const [game, setGame] = useState({});
  const [rating, setRating] = useState(-1);
  const [availableCollections, setAvailableCollections] = useState([]);

  const navigate = useNavigate();
  const location = useLocation();

  const { gameId } = useParams();

  useEffect(() => {
    let isMounted = true;
    const controller = new AbortController();

    const getGame = async () => {
      try {
        const response = await axiosPrivate.get(`/Game/details/${gameId}`, {
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
  }, []);

  return (
    <>
      <div>
        <h2 class="text-center">{game.name}</h2>
        <div class="card h-100 w-75 mx-auto">
          <img class="card-img-top" src={game.photoPath} alt="Game image" />
          <div class="card-body my-auto">
            <div className="d-flex">
              <div>
                <h5 class="card-title">
                  {game.nameRu} / {game.nameEn}{" "}
                  <span className="text-black-50">({game.minAge}+)</span>
                </h5>
                <p class="card-text">
                  From {game.minPlayers} to {game.maxPlayers} participants
                  <br />
                  Time: {game.minPlayTime} - {game.maxPlayTime} minutes
                  <br />
                </p>
              </div>
              <div class="d-flex ms-auto mb-auto">
                {rating === null ? (
                  <StarRating step={1} />
                ) : (
                  <label class="rating-label my-auto">
                    <input
                      class="rating"
                      max="5"
                      step="0.25"
                      style={{ "--value": `${game.averageRating}` }}
                      type="range"
                      value={game.averageRating}
                    />
                  </label>
                )}
                {game.averageRating !== undefined && (
                    <span className="my-auto">
                      {game.averageRating.toFixed(2)}
                    </span>
                  )}
              </div>
              
            </div>

            {auth?.id && (
              <div class="my-3">
                {availableCollections.length === 0 ? (
                  <p>The game is already in all your collections.</p>
                ) : (
                  <button
                    type="button"
                    class="btn btn-warning"
                    data-bs-toggle="modal"
                    data-bs-target="#addCollectionModal"
                  >
                    Add to collection
                  </button>
                )}
              </div>
            )}
            <div class="my-3">
              {auth?.id && (
                <a class="btn btn-secondary btn-sm me-2">
                  {game.name} Round Results
                </a>
              )}
              <Link class="btn btn-secondary btn-sm" to="/games">
                Back to List
              </Link>
            </div>
            <div>
              {auth?.roles?.includes("admin") ?? (
                <>
                  <a class="btn btn-light">Add a Round Result</a>
                  <a class="btn btn-light">Edit</a>
                  <a class="btn btn-light">Delete</a>
                </>
              )}
            </div>
          </div>
          <div class="card-footer text-body-secondary">
            Released in {game.yearOfRelease}
            <br />
          </div>
        </div>
      </div>

      <div
        class="modal fade"
        id="addCollectionModal"
        tabIndex="-1"
        role="dialog"
        aria-labelledby="addCollectionModalLabel"
        aria-hidden="true"
      >
        <div class="modal-dialog" role="document">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title fs-5" id="addCollectionModalLabel">
                Add in collection
              </h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              <form>
                <input type="hidden" name="id" value="@Model.Game.Id" />
                <div class="form-group">
                  <label for="collectionSelect">Select collection</label>
                  <select
                    class="form-control"
                    id="collectionSelect"
                    name="collectionId"
                  >
                    {availableCollections.map((collection, i) => (
                      <option value={collection.id} key={i}>
                        {collection.name}
                      </option>
                    ))}
                  </select>
                </div>
                <input
                  type="submit"
                  value="Add to collection"
                  class="btn btn-warning"
                />
              </form>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default GameDetails;