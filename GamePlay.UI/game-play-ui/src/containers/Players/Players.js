import { useState, useEffect } from "react";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { useNavigate, useLocation } from "react-router-dom";
import useAuth from "hooks/useAuth";
import GameRoundTable from "components/gameRound/GameRoundTable/GameRoundTable";
import { Modal } from "react-bootstrap";
import AddGameRoundForm from "components/gameRound/AddGameRoundForm/AddGameRoundForm";
import { useTranslation } from "react-i18next";
import PlayersTable from "components/gameRound/PlayersTable/PlayersTable";
import AddPlayerForm from "components/gameRound/AddPlayerForm/AddPlayerForm";

const Players = () => {
  const [players, setPlayers] = useState([]);

  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();
  const { t } = useTranslation();

  const navigate = useNavigate();
  const location = useLocation();
  const [show, setShow] = useState(false);

  const handleClose = () => {
    setShow(false);
    getPlayers();
  };

  const handleShow = () => setShow(true);

  useEffect(() => {
    getPlayers();
  }, []);

  const getPlayers = async () => {
    try {
      const response = await axiosPrivate.get(`player`);
      if (response.data.succeeded) {
        setPlayers(response.data.result);
      }
    } catch (err) {
      if (err.name === "CanceledError") {
        return;
      }

      console.error(err);
      navigate("/login", { state: { from: location }, replace: true });
    }
  };

  return (
    <>
      <div className="mx-auto">
        {auth?.accessToken !== undefined && (
          <button
            className="btn btn-primary btn-sm opacity-75 w-25"
            onClick={handleShow}
          >
            {t("forms.add")}
          </button>
        )}
        <PlayersTable players={players} resetPlayers={getPlayers} />
      </div>
      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>
            {t("forms.add")} {t("player.what")}
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <AddPlayerForm handleClose={handleClose} />
        </Modal.Body>
      </Modal>
    </>
  );
};

export default Players;
