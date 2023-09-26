import { useEffect, useState } from "react";
import Moment from "moment";
import { Link, useLocation, useNavigate, useParams } from "react-router-dom";
import useAxiosPrivate from "hooks/useAxiosPrivate";
import { useTranslation } from "react-i18next";

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
  const { t } = useTranslation();

  const { roundId } = useParams();

  useEffect(() => {
    let isMounted = true;
    const controller = new AbortController();

    const getRound = async () => {
      try {
        const response = await axiosPrivate.get(
          `/gameRound/getById/${roundId}`,
          {
            signal: controller.signal,
          }
        );
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
      <h2>{t("roundResult.details")}</h2>
      <div class="card">
        <div class="card-header">
          {t("roundResult.roundId")}: {round.id}
        </div>
        <div class="card-body">
          {t("game.game")}:{" "}
          <Link to={`/gameDetails/${round.game.id}`}>{round.game.name}</Link>
          <br />
          {t("roundResult.date")}: {Moment(round.date).format("DD MMM yyyy")}{" "}
          <br />
          {t("roundResult.creator")}:{" "}
          <Link to={`/userDetails/${round.creatorId}`}>
            {round.creator?.userName}
          </Link>{" "}
          <br />
          <h4>{t("roundResult.players")}: </h4>
          <br />
          <table class="table table-striped">
            <thead>
              <tr>
                <th scope="col">{t("player.name")}</th>
                <th scope="col">{t("player.role")}</th>
                <th scope="col">{t("player.score")}</th>
                <th scope="col">{t("player.isWinner")}?</th>
                <th scope="col">{t("player.userLink")}</th>
              </tr>
            </thead>
            <tbody>
              {round.players.map((player, i) => (
                <tr key={i}>
                  <td>{player.player.name}</td>
                  <td>{player.role === "" ? "–" : player.role}</td>
                  <td>{player.score}</td>
                  <td>
                    {player.isWinner ? (
                      <span>{t("player.yes")}</span>
                    ) : (
                      <span>{t("player.no")}</span>
                    )}
                  </td>
                  <td>
                    {player.player.isRegistered ? (
                      <Link to={`/userDetails/${player.player.userId}`}>
                        {t("userDetails.differentUser.profile")}
                      </Link>
                    ) : (
                      <span>{t("player.notRegistered")}</span>
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
