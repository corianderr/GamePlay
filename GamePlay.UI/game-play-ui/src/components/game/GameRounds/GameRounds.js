import { useState, useEffect } from "react";
import useAxiosPrivate from "../../../hooks/useAxiosPrivate";
import { useNavigate, useLocation } from "react-router-dom";
import useAuth from "hooks/useAuth";

const GameRounds = () => {
  const [rounds, setRounds] = useState([]);
  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    let isMounted = true;
    const controller = new AbortController();

    const getRounds = async () => {
      try {
        const response = await axiosPrivate.get(
          `/gameRound?userId=${auth?.id}`,
          {
            signal: controller.signal,
          }
        );
        console.log(response.data);
        isMounted && setRounds(response.data.result);
      } catch (err) {
        if (err.name === "CanceledError") {
          return;
        }

        console.error(err);
        navigate("/login", { state: { from: location }, replace: true });
      }
    };
    getRounds();

    return () => {
      isMounted = false;
      controller.abort();
    };
  }, []);

  return (
    <>
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
              <td>{round.game.name}</td>
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
                    class="btn btn-secondary"
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
  );
};

export default GameRounds;
