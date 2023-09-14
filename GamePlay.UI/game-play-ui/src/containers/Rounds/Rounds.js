import { useState, useEffect } from "react";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { useNavigate, useLocation } from "react-router-dom";
import useAuth from "hooks/useAuth";
import GameRoundTable from "components/gameRound/GameRoundTable/GameRoundTable";
import { Modal } from "react-bootstrap";
import AddGameRoundForm from "components/gameRound/AddGameRoundForm/AddGameRoundForm";

const Rounds = () => {
  const [rounds, setRounds] = useState([]);

  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();

  const navigate = useNavigate();
  const location = useLocation();
  const [showAddRound, setShowAddRound] = useState(false);

  const handleAddRoundClose = () => setShowAddRound(false);
  const handleAddRoundShow = () => setShowAddRound(true);

  useEffect(() => {
    getRounds();
  }, []);

  const getRounds = async () => {
    try {
      const response = await axiosPrivate.get(`/gameRound?userId=${auth?.id}`);
      if (response.data.succeeded) {
        setRounds(response.data.result);
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
      {auth?.accessToken !== undefined && auth?.roles.includes("admin") && (
        <button
          className="btn btn-primary btn-sm opacity-75 w-25"
          onClick={handleAddRoundShow}
        >
          Add
        </button>
      )}
      <GameRoundTable
        header={"My Rounds"}
        rounds={rounds}
        resetRounds={getRounds}
      />
      <Modal show={showAddRound} onHide={handleAddRoundClose} scrollable={true}>
        <Modal.Header closeButton>
          <Modal.Title>Add game round result</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <AddGameRoundForm handleClose={handleAddRoundClose} />
        </Modal.Body>
      </Modal>
    </>
  );
};

export default Rounds;
