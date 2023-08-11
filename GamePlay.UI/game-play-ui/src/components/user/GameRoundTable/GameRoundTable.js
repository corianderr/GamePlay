import useAuth from "hooks/useAuth";
import { Link } from "react-router-dom";

const GameRoundTable = ({ header, rounds }) => {
  const { auth } = useAuth();

  return (
    <>
      {rounds?.length === 0 ? (
        <h5 className="mt-3">There are no results yet..</h5>
      ) : (
        <>
                    <h2 className="text-center">{header}</h2>
          <table class="table table-striped">
            <thead>
              <tr>
                <th scope="col">#</th>
                <th scope="col">Game</th>
                <th scope="col">Date</th>
                <th scope="col">Place</th>
                {auth?.roles.includes("admin") && <th scope="col">Actions</th>}
              </tr>
            </thead>
            <tbody>
              {rounds.map((round, i) => (
                <tr>
                  <th scope="row">{i + 1}</th>
                  <td><Link to={`/roundDetails/${round.id}`}>{round.game.name}</Link></td>
                  <td>{round.date}</td>
                  <td>{round.place}</td>
                  {auth?.roles.includes("admin") && (
                    <td>
                      <a
                        asp-controller="GameRounds"
                        asp-action="Edit"
                        asp-route-gameRoundId="@Model[i].Id"
                        class="btn btn-secondary"
                      >
                        Edit
                      </a>
                      <a
                        asp-controller="GameRounds"
                        asp-action="Delete"
                        asp-route-id="@Model[i].Id"
                        class="btn btn-secondary ms-2"
                      >
                        Delete
                      </a>
                    </td>
                  )}
                </tr>
              ))}
            </tbody>
          </table>
        </>
      )}
    </>
  );
};

export default GameRoundTable;
