import useAuth from "hooks/useAuth";
import useAxiosPrivate from "hooks/useAxiosPrivate";
import { Link } from "react-router-dom";
import { toast } from "react-toastify";

const GameRoundTable = ({ header, rounds, resetRounds }) => {
  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();

  const handleDelete = async (id) => {
    if (window.confirm("Are you sure you want to delete this round?")) {
      const response = await axiosPrivate.delete(`gameRound/${id}`);
      if (response.data.succeeded) {
        toast.success("Round has been deleted");
        resetRounds();
      }
    }
  };

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
                      <button
                        class="btn btn-secondary ms-2"
                        onClick={()  => handleDelete(round.id)}
                      >
                        Delete
                      </button>
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
