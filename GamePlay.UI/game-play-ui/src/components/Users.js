import { useState, useEffect } from "react";
import useAxiosPrivate from "../hooks/useAxiosPrivate";
import { useNavigate, useLocation } from "react-router-dom";
import useAuth from "../hooks/useAuth";

const Users = () => {
    const [users, setUsers] = useState([]);
    const axiosPrivate = useAxiosPrivate();
    const navigate = useNavigate();
    const location = useLocation();
    const {auth} = useAuth();

    useEffect(() => {
        let isMounted = true;
        const controller = new AbortController();

        const getUsers = async () => {
            try {
                const response = await axiosPrivate.get('/User', {
                    signal: controller.signal
                });
                console.log(response.data);
                isMounted && setUsers(response.data.result);
            } catch (err) {
                if (err.name === 'CanceledError') {
                    // Request was cancelled, no need to handle the error
                    return;
                }

                console.error(err);
                navigate('/login', { state: { from: location }, replace: true });
            }
        }

        getUsers();

        return () => {
            isMounted = false;
            controller.abort();
        }
    }, [])

    return (
        <>
          <h2>Users</h2>
          <div className="container mt-3 mb-4">
            <div className="col-lg-9 mt-4 mt-lg-0">
              <div className="row">
                <div className="col-md-12">
                  <div className="user-dashboard-info-box table-responsive mb-0 bg-white p-4 shadow-sm">
                    <table className="table manage-candidates-top mb-0">
                      <thead>
                        <tr>
                          <th>Username</th>
                        </tr>
                      </thead>
                      <tbody>
                        {users.map((user, i) => (
                          <tr className="candidates-list" key={i}>
                            <td className="title">
                              <div className="thumb">
                                <img className="img-fluid" src={user.photoPath} alt="avatar" />
                              </div>
                              <div className="candidate-list-details">
                                <div className="candidate-list-info">
                                  <div className="candidate-list-title">
                                    <h5 className="mb-0">
                                      {user.id === auth?.id && <span>My Profile: </span>}
                                      {/* Add link to detailed page */}
                                      <a href="#">{user.username}</a>
                                    </h5>
                                  </div>
                                  <div className="candidate-list-option">
                                    <p>{user.email}</p>
                                  </div>
                                </div>
                              </div>
                            </td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </>
      );
};

export default Users;