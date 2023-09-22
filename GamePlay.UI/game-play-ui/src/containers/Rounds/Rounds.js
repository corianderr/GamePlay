import { useState, useEffect } from "react";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { useNavigate, useLocation } from "react-router-dom";
import useAuth from "hooks/useAuth";
import GameRoundTable from "components/gameRound/GameRoundTable/GameRoundTable";
import { Modal } from "react-bootstrap";
import AddGameRoundForm from "components/gameRound/AddGameRoundForm/AddGameRoundForm";
import { useTranslation } from "react-i18next";

const Rounds = () => {
  const [rounds, setRounds] = useState([]);

  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();
  const { t } = useTranslation();

  const navigate = useNavigate();
  const location = useLocation();
  const [showAddRound, setShowAddRound] = useState(false);

  const handleAddRoundClose = () => {
    setShowAddRound(false);
    getRounds();
  }

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
      {auth?.accessToken !== undefined && (
        <button
          className="btn btn-primary btn-sm opacity-75 w-25"
          onClick={handleAddRoundShow}
        >
          {t("forms.add")}
        </button>
      )}
      <GameRoundTable
        header={t("roundResult.myRounds")}
        rounds={rounds}
        resetRounds={getRounds}
      />
      <Modal show={showAddRound} onHide={handleAddRoundClose} scrollable={true}>
        <Modal.Header closeButton>
          <Modal.Title>{t("forms.add")} {t("roundResult.what")}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <AddGameRoundForm handleClose={handleAddRoundClose} />
        </Modal.Body>
      </Modal>
    </>
  );
};

export default Rounds;
