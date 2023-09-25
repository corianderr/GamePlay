import { useTranslation } from "react-i18next";
import UserRow from "../UserRow/UserRow";
import "./UserList.css";
import { useState } from "react";
import { useEffect } from "react";
import { Pagination } from "@mui/material";

const UserList = ({ header, users, relations }) => {
  const { t } = useTranslation();

  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);
  const [subset, setSubset] = useState([]);
  const itemsPerPage = 10;
  
  const handlePageChange = (event, value) => {
    setCurrentPage(value);
  };

  useEffect(() => {
    setTotalPages(Math.ceil(users.length / itemsPerPage));
    updateSubset();
  }, [users]);

  useEffect(() => {
      updateSubset();
  }, [currentPage]);

  const updateSubset = () => {
    const startIndex = (currentPage - 1) * itemsPerPage;
    const endIndex = startIndex + itemsPerPage;
    setSubset(users.slice(startIndex, endIndex));
  }

  return (
    <>
      {users?.length === 0 ? (
        <h5 className="mt-3">{t("user.noElements1")} {header} {t("user.noElements2")}..</h5>
      ) : (
        <>
          <div className="container mb-4">
            <h2 className="text-center">{header}</h2>
            <div className="col-lg-9 mt-3 mx-auto mt-3">
              <div className="row">
                <div className="col-md-12">
                  <div className="user-dashboard-info-box table-responsive mb-0 bg-white p-4 shadow-sm">
                    <table className="table manage-candidates-top mb-0">
                      <tbody>
                        {relations !== undefined
                          ? subset.map((user, i) => (
                              <UserRow
                                user={user}
                                key={i}
                                relation={relations[i]}
                              />
                            ))
                          : subset.map((user, i) => (
                              <UserRow user={user} key={i} />
                            ))}
                      </tbody>
                    </table>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </>
      )}
      {totalPages > 1 && (<Pagination count={totalPages} page={currentPage} onChange={handlePageChange} />)}
    </>
  );
};

export default UserList;
