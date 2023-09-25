import useAuth from "hooks/useAuth";
import useAxiosPrivate from "hooks/useAxiosPrivate";
import { Modal } from "react-bootstrap";
import { Link } from "react-router-dom";
import { toast } from "react-toastify";
import EditGameRoundForm from "../EditGameRoundForm/EditGameRoundForm";
import { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useTranslation } from "react-i18next";
import { useEffect } from "react";
import { TablePagination } from "@mui/material";

const GameRoundTable = ({ header, rounds, resetRounds }) => {
  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();
  const [show, setShow] = useState(false);
  const [gameRoundId, setGameRoundId] = useState(null);
  const { t } = useTranslation();

  const [currentPage, setCurrentPage] = useState(0);
  const [subset, setSubset] = useState([]);
  const [rowsPerPage, setRowsPerPage] = useState(10);

  const handlePageChange = (event, value) => {
    setCurrentPage(value);
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setCurrentPage(0);
  };

  useEffect(() => {
    updateSubset();
  }, [rounds]);

  useEffect(() => {
    updateSubset();
  }, [currentPage]);

  const updateSubset = () => {
    const startIndex = currentPage * rowsPerPage;
    const endIndex = startIndex + rowsPerPage;
    setSubset(rounds.slice(startIndex, endIndex));
  };

  const handleClose = () => {
    setShow(false);
    resetRounds();
  };

  const handleShow = (id) => {
    setGameRoundId(id);
    setShow(true);
  };

  const handleDelete = async (id) => {
    if (window.confirm(t("roundResult.confirmDeleteMes"))) {
      const response = await axiosPrivate.delete(`gameRound/${id}`);
      if (response.data.succeeded) {
        toast.success(t("roundResult.deleteMes"));
        resetRounds();
      } else {
        toast.error("Error...");
      }
    }
  };

  return (
    <>
      {rounds?.length === 0 ? (
        <h5 className="mt-3">{t("roundResult.noRounds")}</h5>
      ) : (
        <>
          <h2 className="text-center">{header}</h2>
          <table className="table table-striped bg-light">
            <thead>
              <tr>
                <th scope="col">#</th>
                <th scope="col">{t("game.game")}</th>
                <th scope="col">{t("roundResult.date")}</th>
                <th scope="col">{t("roundResult.place")}</th>
                {auth?.roles.includes("admin") && (
                  <th scope="col">{t("roundResult.actions")}</th>
                )}
              </tr>
            </thead>
            <tbody>
              {subset.map((round, i) => (
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
      {rounds.length !== 0 && (
        <TablePagination
          component="div"
          count={rounds.length}
          page={currentPage}
          onPageChange={handlePageChange}
          rowsPerPage={rowsPerPage}
          onRowsPerPageChange={handleChangeRowsPerPage}
        />
      )}
      <Modal show={show} onHide={handleClose} scrollable={true}>
        <Modal.Header closeButton>
          <Modal.Title>
            {t("forms.edit")} {t("roundResult.what")}
          </Modal.Title>
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
