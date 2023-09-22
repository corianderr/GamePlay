import { useTranslation } from "react-i18next";
import UserRow from "../UserRow/UserRow";
import "./UserList.css";

const UserList = ({ header, users, relations }) => {
  const { t } = useTranslation();

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
                          ? users.map((user, i) => (
                              <UserRow
                                user={user}
                                key={i}
                                relation={relations[i]}
                              />
                            ))
                          : users.map((user, i) => (
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
    </>
  );
};

export default UserList;
