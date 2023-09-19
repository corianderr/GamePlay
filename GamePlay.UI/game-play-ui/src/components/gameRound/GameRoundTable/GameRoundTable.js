import useAuth from "hooks/useAuth";
import useAxiosPrivate from "hooks/useAxiosPrivate";
import { Modal } from "react-bootstrap";
import { Link } from "react-router-dom";
import { toast } from "react-toastify";
import EditGameRoundForm from "../EditGameRoundForm/EditGameRoundForm";
import { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const GameRoundTable = ({ header, rounds, resetRounds }) => {
  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();
  const [show, setShow] = useState(false);
  const [gameRoundId, setGameRoundId] = useState(null);

  const handleClose = () => {
    setShow(false);
    resetRounds();
  }

  const handleShow = (id) => {
    setGameRoundId(id);
    setShow(true);
  };

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
          <table className="table table-striped bg-light">
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
                <tr key={i}>
                  <th scope="row">{i + 1}</th>
                  <td>
                    <Link to={`/roundDetails/${round.id}`}>
                      {round.game.name}
                    </Link>
                  </td>
                  <td>{round.date}</td>
                  <td>{round.place}</td>
                  {auth?.roles.includes("admin") && (
                    <td>
                      <button
                        className="btn"
                        onClick={() => handleShow(round.id)}
                      >
                        <FontAwesomeIcon icon="fa-solid fa-pen-to-square" />
                      </button>
                      <button
                        className="btn"
                        onClick={() => handleDelete(round.id)}
                      >
                        <FontAwesomeIcon icon="fa-solid fa-trash" />
                      </button>
                    </td>
                  )}
                </tr>
              ))}
            </tbody>
          </table>
        </>
      )}
      <Modal show={show} onHide={handleClose} scrollable={true}>
        <Modal.Header closeButton>
          <Modal.Title>Edit game round result</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <EditGameRoundForm
            gameRoundId={gameRoundId}
            handleClose={handleClose}
          />
        </Modal.Body>
      </Modal>
    </>
  );
};

export default GameRoundTable;
