import { useState, useEffect } from "react";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { useNavigate, useLocation } from "react-router-dom";
import GameList from "../../components/game/GameList/GameList";
import useAuth from "hooks/useAuth";
import { Modal } from "react-bootstrap";
import CreateGameForm from "components/game/CreateGameForm/CreateGameForm";

const Games = () => {
  const [games, setGames] = useState([]);
  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();
  const location = useLocation();
  const [show, setShow] = useState(false);
  const { auth } = useAuth();

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
          Create Game
        </button>
      )}
      <GameList games={games} header={"Games"} />
      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Add Game</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <CreateGameForm handleClose={handleClose} updateData={getGames} />
        </Modal.Body>
      </Modal>
    </>
  );
};

export default Games;
