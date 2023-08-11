import React, { useEffect, useState } from "react";
import RelationButton from "../../components/user/RelationButton/RelationButton";
import useAuth from "../../hooks/useAuth";
import { Link, useLocation, useNavigate, useParams } from "react-router-dom";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { toast } from "react-toastify";

const UserDetails = () => {
  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();

  const [user, setUser] = useState({});
  const [relation, setRelation] = useState(-1);
  const [collections, setCollections] = useState([]);

  const navigate = useNavigate();
  const location = useLocation();

  const { userId } = useParams();

  useEffect(() => {
    let isMounted = true;
    const controller = new AbortController();

    const getUser = async () => {
      try {
        const response = await axiosPrivate.get(`/user/details/${userId}`, {
          signal: controller.signal,
        });
        if (isMounted) {
          var data = response.data.result;
          setUser(data.user);
          setRelation(data.relationOption);
          setCollections(data.collections);
        }
      } catch (err) {
        if (err.name === "CanceledError") {
          return;
        }

        console.error(err);
        navigate("/login", { state: { from: location }, replace: true });
      }
    };

    if (relation === -1) {
      getUser();
    }

    return () => {
      isMounted = false;
      controller.abort();
    };
  }, [userId, collections]);

  const updateUser = () => {
    const getUser = async () => {
      const response = await axiosPrivate.get(`/user/getById/${userId}`);
      setUser(response.data.result);
    };
    getUser();
  };

  const handleDelete = async (id) => {
    if (window.confirm("Are you sure you want to delete this collection?")) {
      const response = await axiosPrivate.delete(`collection/${id}`);
      if (response.data.succeeded) {
        toast.success("Collection has been deleted");
        setCollections([]);
      }else{
        console.log(response.data);
        toast.error(JSON.stringify(response.data.errors).replace(/[{}[\]"]/g, ' '))
      }
    }
  };

  return (
    <>
      <div className="d-flex justify-content-center align-items-center mb-3">
        <div className="profile-card">
          <div className="d-flex text-center">
            <div className="mx-auto profile">
              <img
                src={user.photoPath}
                className="rounded-circle"
                width="80"
                alt="User"
              />
            </div>
          </div>
          <div className="mt-2 text-center">
            <h4 className="mb-0">{user.userName}</h4>
            <span className="text-muted d-block mb-2">{user.email}</span>
            {user.id === auth?.id ? (
              <a className="btn btn-primary btn-sm follow" href="/Users/Edit">
                Edit profile
              </a>
            ) : (
              <RelationButton
                relation={relation}
                userId={user.id}
                userUpdate={updateUser}
              />
            )}

            <div className="d-flex justify-content-between align-items-center mt-4 px-4">
              <div className="stats">
                <h6 className="mb-0">Followers</h6>
                <span>
                  <Link to={`/followers/${user.id}`}>
                    {user.followersCount}
                  </Link>
                </span>
              </div>
              <div className="stats">
                <h6 className="mb-0">Friends</h6>
                <span>
                  <Link to={`/friends/${user.id}`}>{user.friendsCount}</Link>
                </span>
              </div>
            </div>
          </div>
        </div>
      </div>

      {collections.length === 0 ? (
        user.id === auth?.id ? (
          <>
            <h5>
              I don't have any collections yet :( <br />
              Add one{" "}
              <Link className="text-black">
                <FontAwesomeIcon
                  icon="fa-solid fa-square-plus"
                  className="ms-2 opacity-75"
                />
              </Link>
            </h5>
          </>
        ) : (
          <h5>{user.userName} does not have any collections yet :(</h5>
        )
      ) : user.id === auth?.id ? (
        <div className="d-flex">
          <h3>
            My Collections{" "}
            <Link className="text-black">
              <FontAwesomeIcon
                icon="fa-solid fa-square-plus"
                className="ms-2 opacity-75"
              />
            </Link>
          </h3>
        </div>
      ) : (
        <h3>{user.userName}'s Collections</h3>
      )}
      <div className="row">
        {collections.map((item) => (
          <div className="col-sm-4 mb-3" key={item.id}>
            <div className="card h-100 text-center">
              <div className="card-body my-auto color">
                <h5 className="card-title">
                  <Link
                    to={`/collectionDetails/${item.id}`}
                    className="text-black"
                  >
                    {item.name}
                  </Link>
                </h5>
                {user.id === auth?.id && (
                  <div>
                    <Link
                      className="btn btn-light"
                      to={`/collections/edit/${item.id}`}
                    >
                      Edit
                    </Link>
                    <button
                      className="btn btn-light "
                      onClick={() => handleDelete(item.id)}
                    >
                      Delete
                    </button>
                  </div>
                )}
              </div>
            </div>
          </div>
        ))}
      </div>
    </>
  );
};

export default UserDetails;
