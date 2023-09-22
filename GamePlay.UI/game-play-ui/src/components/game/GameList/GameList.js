import { Link, useNavigate } from "react-router-dom";
import "./GameList.css";
import { useTranslation } from "react-i18next";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import useAxiosPrivate from "hooks/useAxiosPrivate";
import { toast } from "react-toastify";

const GameList = ({ header, games, collectionId, refreshGames }) => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const axiosPrivate = useAxiosPrivate();

  const redirectToGameDetails = (gameId) => {
    navigate(`/gameDetails/${gameId}`);
  };

  const deleteFromCollectoin = async (gameId) => {
    if (!window.confirm(`Are you sure you want to remove this game from ${header} collection?`)){
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

  return (
    <>
      {games?.length === 0 ? (
        <h5 className="mt-3">{t("game.noGames")}</h5>
      ) : (
        <>
          <div className="container">
            <h2 className="text-center">{header}</h2>
            <div className="row mt-3">
              {games.map((game, i) => (
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
                      <div className="d-flex justify-content-end install mt-auto">
                        <span>
                          {t("game.releasedIn")} {game.yearOfRelease}
                        </span>
                      </div>
                    </div>
                    {collectionId !== undefined && (
                      <div className="bg-danger text-center" style={{borderRadius: "0 0 5px 5px"}}>
                        <button
                          className="btn"
                          onClick={() => deleteFromCollectoin(game.id)}
                        >
                          <FontAwesomeIcon className="text-light" icon="fa-solid fa-trash" />
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
    </>
  );
};

export default GameList;
