import React, { useEffect, useState } from "react";
import RelationButton from "./RelationButton";
import useAuth from "../../hooks/useAuth";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";

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

    const getUsers = async () => {
      try {
        const response = await axiosPrivate.get(`/User/details/${userId}`, {
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
      getUsers();
    }

    return () => {
      isMounted = false;
      controller.abort();
    };
  }, [userId, collections]);

  const updateUser = () => {
    const getUser = async () => {
      const response = await axiosPrivate.get(`/User/getById/${userId}`);
      setUser(response.data.result);
    }
    getUser();
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
            <h4 className="mb-0">{user.username}</h4>
            <span className="text-muted d-block mb-2">{user.email}</span>
            {user.id === auth?.id ? (
              <a className="btn btn-primary btn-sm follow" href="/Users/Edit">
                Edit profile
              </a>
            ) : (
              <RelationButton relation={relation} userId={user.id} userUpdate={updateUser}/>
            )}

            <div className="d-flex justify-content-between align-items-center mt-4 px-4">
              <div className="stats">
                <h6 className="mb-0">Followers</h6>
                <span>{user.followersCount}</span>
              </div>
              <div className="stats">
                <h6 className="mb-0">Friends</h6>
                <span>{user.friendsCount}</span>
              </div>
            </div>
          </div>
        </div>
      </div>

      {collections.length === 0 ? (
        user.id === auth?.id ? (
          <>
            <a className="btn btn-light mb-3" href="/Collections/Create">
              Create Collection
            </a>
            <h5>I don't have any collections yet :(</h5>
          </>
        ) : (
          <h5>{user.username} does not have any collections yet :(</h5>
        )
      ) : user.id === auth?.id ? (
        <>
          <a className="btn btn-light mb-3" href="/Collections/Create">
            Create Collection
          </a>
          <h3>My Collections</h3>
        </>
      ) : (
        <h3>{user.username}'s Collections</h3>
      )}

      {/* TODO: Fix links in block above */}
      <div className="row">
        {collections.map((item) => (
          <div
            className="col-sm-4 mb-3"
            key={item.id}
            style={{ cursor: "pointer" }}
          >
            <div className="card h-100 text-center">
              <div className="card-body my-auto color">
                <h5 className="card-title">{item.name}</h5>
                {user.id === auth?.id && (
                  <div>
                    <a
                      className="btn btn-light"
                      href={`/Collections/Edit/${item.id}`}
                    >
                      Edit
                    </a>
                    <a
                      className="btn btn-light"
                      href={`/Collections/Delete/${item.id}`}
                    >
                      Delete
                    </a>
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
