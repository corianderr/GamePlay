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
      const response = await axiosPrivate.post("/User/logout");
      setAuth({});
    };
    clearCookies();
  };

  return (
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
      <div class="container">
        <Link to="/users" className="logo navbar-brand">
          Game Play
        </Link>
        <button
          class="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarNav"
          aria-controls="navbarNav"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav w-100">
            <li>
              <Link to="/games" className="nav-link active">
                Games
              </Link>
            </li>
            {auth?.accessToken ? (
              <>
                <li class="nav-item">
                  <Link to="/users" className="nav-link">
                    Users
                  </Link>
                </li>
                <li class="nav-item">
                  <a className="nav-link" href="#">
                    Game Rounds
                  </a>
                </li>
                <li class="ms-auto nav-item">
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
                <li class="nav-item">
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
