import { Link } from "react-router-dom";
import useAuth from "hooks/useAuth";
import useAxiosPrivate from "hooks/useAxiosPrivate";
import { useEffect } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

export default function NavMenu() {
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
                Games
              </Link>
            </li>
            {auth?.accessToken ? (
              <>
                <li className="nav-item">
                  <Link to="/users" className="nav-link">
                    Users
                  </Link>
                </li>
                <li className="nav-item">
                  <a className="nav-link" href="#">
                    Game Rounds
                  </a>
                </li>
                <li className="ms-auto nav-item">
                  <Link to={`/notifications`}>
                    <FontAwesomeIcon
                      icon="fa-solid fa-bell"
                      className="nav-link bell"
                    />
                  </Link>
                </li>
                <li className="nav-item">
                  <Link to={`/userDetails/${auth?.id}`} className="nav-link">
                    Hello, {auth?.username}!
                  </Link>
                </li>
                <li className="nav-item">
                  <span
                    onClick={logout}
                    className="nav-link"
                    style={{ cursor: "pointer" }}
                  >
                    Logout
                  </span>
                </li>
              </>
            ) : (
              <li className="ms-auto nav-item">
                <Link to="/login" className="nav-link">
                  Login
                </Link>
              </li>
            )}
          </ul>
        </div>
      </div>
    </nav>
  );
}
