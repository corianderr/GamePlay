import { Link } from "react-router-dom";
import "./GameList.css";

const GameList = ({ header, games }) => {
  return (
    <>
      {games?.length === 0 ? (
        <h5 className="mt-3">There are no games yet..</h5>
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
                      From {game.minPlayers} to {game.maxPlayers} participants{" "}
                      <br />
                      Time: {game.minPlayTime} - {game.maxPlayTime} minutes
                    </h6>
                    <div className="d-flex justify-content-between install mt-auto">
                      <span>Released in {game.yearOfRelease}</span>
                      <Link
                        className="text-primary"
                        to={`/gameDetails/${game.id}`}
                      >
                        View&nbsp;<i className="fa fa-angle-right"></i>
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
