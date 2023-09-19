import { Link } from "react-router-dom";
import "./GameList.css";
import { useTranslation } from "react-i18next";

const GameList = ({ header, games }) => {
  const { t } = useTranslation();

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
                <div className="col-md-4 col-sm-6 mb-3" key={i}>
                  <div className="card p-3 h-100">
                    <div className="d-flex flex-row mb-3">
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
                    <h6 className="mb-3">
                    {t("game.from")} {game.minPlayers} {t("game.to")} {game.maxPlayers} {t("game.participants")}{" "}
                      <br />
                      {t("game.time")}: {game.minPlayTime} - {game.maxPlayTime} {t("game.minutes")}
                    </h6>
                    <div className="d-flex justify-content-between install mt-auto">
                      <span>{t("game.releasedIn")} {game.yearOfRelease}</span>
                      <Link
                        className="text-primary"
                        to={`/gameDetails/${game.id}`}
                      >
                        {t("game.view")}&nbsp;<i className="fa fa-angle-right"></i>
                      </Link>
                    </div>
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
