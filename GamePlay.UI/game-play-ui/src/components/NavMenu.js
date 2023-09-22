import { Link } from "react-router-dom";
import useAuth from "hooks/useAuth";
import useAxiosPrivate from "hooks/useAxiosPrivate";
import { useEffect } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useTranslation } from "react-i18next";

export default function NavMenu() {
  const { t, i18n } = useTranslation();
  const { auth, setAuth } = useAuth();
  const axiosPrivate = useAxiosPrivate();

  useEffect(() => {
    console.log(auth);
  }, [auth]);

  const logout = () => {
    const clearCookies = async () => {
      const response = await axiosPrivate.post("/user/logout");
      setAuth({});
    };
    clearCookies();
  };

  const changeLanguage = (language) => {
    i18n.changeLanguage(language);
  };

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
      <div className="container">
        <Link to="/games" className="logo navbar-brand">
          Game Play
        </Link>
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarNav"
          aria-controls="navbarNav"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav w-100">
            <li>
              <Link to="/games" className="nav-link active">
                {t("navMenu.games")}
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/users" className="nav-link">
                {t("navMenu.users")}
              </Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/rounds">
                {t("navMenu.myRounds")}
              </Link>
            </li>
            {auth?.accessToken ? (
              <>
                <li className="ms-lg-auto nav-item">
                  <Link to={`/notifications`}>
                    <FontAwesomeIcon
                      icon="fa-solid fa-bell"
                      className="nav-link bell"
                    />
                  </Link>
                </li>
                <li className="nav-item">
                  <Link to={`/userDetails/${auth?.id}`} className="nav-link">
                    {t("navMenu.hello")}, {auth?.username}!
                  </Link>
                </li>
                <li className="nav-item">
                  <span
                    onClick={logout}
                    className="nav-link"
                    style={{ cursor: "pointer" }}
                  >
                    {t("navMenu.logout")}
                  </span>
                </li>
              </>
            ) : (
              <li className="ms-lg-auto nav-item">
                <Link to="/login" className="nav-link">
                  {t("navMenu.login")}
                </Link>
              </li>
            )}
            <li className="nav-item align-self-lg-center">
              <div className="btn-group" role="group" aria-label="Basic example">
                <button
                  className="btn btn-dark btn-sm pe-1"
                  onClick={() => changeLanguage("en")}
                >
                  <img width={15} height={15} src="flags\united-states.png" alt="English"></img>
                </button>
                <button
                  className="btn btn-dark btn-sm ps-1"
                  onClick={() => changeLanguage("ru")}
                >
                  <img width={15} height={15} src="flags\russia.png" alt="Русский"></img>
                </button>
              </div>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
}
