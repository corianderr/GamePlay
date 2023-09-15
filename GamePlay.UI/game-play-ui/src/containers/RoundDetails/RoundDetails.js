import { useEffect, useState } from "react";
import Moment from "moment";
import { Link, useLocation, useNavigate, useParams } from "react-router-dom";
import useAxiosPrivate from "hooks/useAxiosPrivate";

const RoundDetails = () => {
  const [round, setRound] = useState({
    id: null,
    game: {},
    date: null,
    players: [],
  });

  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();
  const location = useLocation();

  const { roundId } = useParams();


  useEffect(() => {
    let isMounted = true;
    const controller = new AbortController();

    const getRound = async () => {
      try {
        const response = await axiosPrivate.get(`/gameRound/getById/${roundId}`, {
          signal: controller.signal,
        });
        if (isMounted) {
          setRound(response.data.result);
        }
      } catch (err) {
        if (err.name === "CanceledError") {
          return;
        }

        console.error(err);
        navigate("/login", { state: { from: location }, replace: true });
      }
    };

    getRound();

    return () => {
      isMounted = false;
      controller.abort();
    };
  }, []);

  return (
    <>
      <h2>Round Details</h2>
      <div class="card">
        <div class="card-header">Round Id: {round.id}</div>
        <div class="card-body">
          Game:{" "}
          <Link to={`/gameDetails/${round.game.id}`}
          >
            {round.game.name}
          </Link>
          <br />
          Date: {Moment(round.date).format("dd MMM yyyy")} <br />
          <h4>Participants: </h4>
          <br />
          <table class="table table-striped">
            <thead>
              <tr>
                <th scope="col">Name</th>
                <th scope="col">Role</th>
                <th scope="col">Score</th>
                <th scope="col">Is Winner?</th>
                <th scope="col">User Profile Link</th>
              </tr>
            </thead>
            <tbody>
              {round.players.map((player, i) => (
                <tr key={i}>
                  <td>{player.player.name}</td>
                  <td>{player.role === "" ? "–" : ""}</td>
                  <td>{player.score}</td>
                  <td>
                    {player.isWinner ? <span>YES</span> : <span>NO</span>}
                  </td>
                  <td>
                    {player.isRegistered ? (
                      <Link to={`/userDetails/${player.userId}`}>
                        Profile
                      </Link>
                    ) : (
                      <span>User is not registered.</span>
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </>
  );
};

export default RoundDetails;
