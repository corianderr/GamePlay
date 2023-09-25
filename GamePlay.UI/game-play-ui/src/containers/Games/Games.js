import { useState, useEffect } from "react";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { useNavigate, useLocation } from "react-router-dom";
import GameList from "../../components/game/GameList/GameList";
import useAuth from "hooks/useAuth";
import { Modal } from "react-bootstrap";
import AddGameForm from "components/game/AddGameForm/AddGameForm";
import { useTranslation } from "react-i18next";

const Games = () => {
  const [games, setGames] = useState([]);
  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();
  const location = useLocation();
  const [show, setShow] = useState(false);
  const { auth } = useAuth();
  const { t } = useTranslation();

  const handleClose = () => {
    setShow(false);
  };

  const handleShow = () => {
    setShow(true);
  };

  const getGames = async () => {
    try {
      const response = await axiosPrivate.get("/game");
      console.log(response.data);
      setGames(response.data.result);
    } catch (err) {
      if (err.name === "CanceledError") {
        return;
      }

      console.error(err);
      navigate("/login", { state: { from: location }, replace: true });
    }
  };

  useEffect(() => {
    getGames();
  }, []);

  return (
    <>
      {auth?.accessToken !== undefined && auth?.roles.includes("admin") && (
        <button className="btn btn-primary btn-sm opacity-75 w-25" onClick={handleShow}>
          {t("forms.add")}
        </button>
      )}
      <GameList games={games} header={t("navMenu.games")} />
      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>{t("forms.add")} {t("game.what")}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <AddGameForm handleClose={handleClose} updateData={getGames} />
        </Modal.Body>
      </Modal>
    </>
  );
};

export default Games;
