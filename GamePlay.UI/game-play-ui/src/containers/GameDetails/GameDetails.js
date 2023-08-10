import React, { useEffect, useState } from "react";
import useAuth from "../../hooks/useAuth";
import { Link, useLocation, useNavigate, useParams } from "react-router-dom";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import StarRating from "../../components/game/StarRating/StarRating";
import { toast } from "react-toastify";

const GameDetails = () => {
  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();

  const [game, setGame] = useState({});
  const [rating, setRating] = useState(-1);
  const [availableCollections, setAvailableCollections] = useState([]);
  const [collectionId, setCollectionId] = useState(null);
  const [errMsg, setErrMsg] = useState('');

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

  const handleSubmit = async (e) => {
    e.preventDefault();

    if(collectionId === null){
      setErrMsg("You should choose at least one collection.");
      return;
    }

    const response = await axiosPrivate.post(
      `collection/addGame?id=${gameId}&collectionId=${collectionId}`,
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    );
    resetRating();
  };

  useEffect(() => {
    setErrMsg('');
  }, [collectionId])

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
                    data-bs-toggle="modal"
                    data-bs-target="#addCollectionModal"
                  >
                    Add to collection
                  </button>
                )}
              </div>
            )}
            <div className="my-3">
              {auth?.id && (
                <Link className="btn-sm me-2 text-black-50">
                  <FontAwesomeIcon icon="fa-solid fa-square-poll-vertical" size="2xl" style={{color: "#fdce3f",}} /> Results
                </Link>
              )}
            </div>
            <div>
              {auth?.roles?.includes("admin") ?? (
                <>
                  <a className="btn btn-light">Add a Round Result</a>
                  <a className="btn btn-light">Edit</a>
                  <a className="btn btn-light">Delete</a>
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

      <div
        className="modal fade"
        id="addCollectionModal"
        tabIndex="-1"
        role="dialog"
        aria-labelledby="addCollectionModalLabel"
        aria-hidden="true"
      >
        <div className="modal-dialog" role="document">
          <div className="modal-content">
            <div className="modal-header">
              <h5 className="modal-title fs-5" id="addCollectionModalLabel">
                Add in collection
              </h5>
              <button
                type="button"
                className="btn-close"
                data-bs-dismiss="modal"
                aria-label="Close"
              ></button>
            </div>
            <div className="modal-body">
            <p  className={errMsg ? "errmsg" : "offscreen"} aria-live="assertive">{errMsg}</p>
              <form onSubmit={handleSubmit}>
                <div className="form-group">
                  <label htmlFor="collectionSelect">Select collection</label>
                  <select
                    className="form-control"
                    id="collectionSelect"
                    name="collectionId"
                    onChange={(e) => {
                      console.log(e.target.value);
                      setCollectionId(e.target.value);
                    }}
                    required
                  >
                    <option selected="true" disabled="disabled">
                      Choose Collection
                    </option>
                    {availableCollections.map((collection, i) => (
                      <option value={collection.id} key={i}>
                        {collection.name}
                      </option>
                    ))}
                  </select>
                </div>
                <button className="btn btn-warning">Add to collection</button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default GameDetails;
