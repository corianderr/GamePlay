import useAuth from "hooks/useAuth";
import useAxiosPrivate from "hooks/useAxiosPrivate";
import { toast } from "react-toastify";
import { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useTranslation } from "react-i18next";
import { useEffect } from "react";
import { TablePagination } from "@mui/material";
import { Link } from "react-router-dom";

const PlayersTable = ({ players, resetPlayers }) => {
  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();
  // TODO: implement logic to edit player
  // const [show, setShow] = useState(false);
  // const [gameRoundId, setGameRoundId] = useState(null);
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
  }, [players]);

  useEffect(() => {
    updateSubset();
  }, [currentPage]);

  const updateSubset = () => {
    const startIndex = currentPage * rowsPerPage;
    const endIndex = startIndex + rowsPerPage;
    setSubset(players.slice(startIndex, endIndex));
  };

  // const handleClose = () => {
  //   setShow(false);
  //   resetPlayers();
  // };

  // const handleShow = (id) => {
  //   setGameRoundId(id);
  //   setShow(true);
  // };

  const handleDelete = async (id) => {
    if (window.confirm(t("player.confirmDeleteMes"))) {
      const response = await axiosPrivate.delete(`player/${id}`);
      if (response.data.succeeded) {
        toast.success(t("player.deleteMes"));
        resetPlayers();
      } else {
        toast.error("Error...");
      }
    }
  };

  return (
    <d>
      {players?.length === 0 ? (
        <h5 className="mt-3">{t("player.noPlayers")}</h5>
      ) : (
        <>
          <h2 className="text-center mb-3">Players</h2>
          <div className="table-responsive">
            <table class="table table-striped bg-light">
              <thead>
                <tr>
                  <th scope="col">#</th>
                  <th scope="col">{t("player.name")}</th>
                  <th scope="col">{t("player.userLink")}</th>
                  <th scope="col">{t("roundResult.actions")}</th>
                </tr>
              </thead>
              <tbody>
                {subset.map((player, i) => (
                  <tr key={i}>
                    <td>{i + 1}</td>
                    <td>{player.name}</td>
                    <td>
                      {player.isRegistered ? (
                        <Link to={`/userDetails/${player.userId}`}>
                          {t("userDetails.differentUser.profile")}
                        </Link>
                      ) : (
                        <span>{t("player.notRegistered")}</span>
                      )}
                    </td>
                    <td>
                      {/* <button
                        className="btn"
                        onClick={() => handleShow(round.id)}
                      >
                        <FontAwesomeIcon icon="fa-solid fa-pen-to-square" />
                      </button> */}
                      <button
                        className="btn"
                        onClick={() => handleDelete(player.id)}
                      >
                        <FontAwesomeIcon icon="fa-solid fa-trash" />
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </>
      )}
      {players.length !== 0 && (
        <TablePagination
          component="div"
          count={players.length}
          page={currentPage}
          onPageChange={handlePageChange}
          rowsPerPage={rowsPerPage}
          onRowsPerPageChange={handleChangeRowsPerPage}
        />
      )}
    </d>
  );
};

export default PlayersTable;
