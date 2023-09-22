import React, { useEffect, useState } from "react";
import RelationButton from "../../components/user/RelationButton/RelationButton";
import useAuth from "../../hooks/useAuth";
import { Link, useLocation, useNavigate, useParams } from "react-router-dom";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { toast } from "react-toastify";
import { Button, Form, Modal } from "react-bootstrap";
import { useTranslation } from "react-i18next";

const UserDetails = () => {
  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();
  const { t } = useTranslation();

  const [user, setUser] = useState({});
  const [relation, setRelation] = useState(-1);
  const [collections, setCollections] = useState([]);
  const [editShow, setEditShow] = useState(false);
  const [addShow, setAddShow] = useState(false);
  const [editProfileShow, setEditProfileShow] = useState(false);
  const [file, setFile] = useState(null);
  const [form, setForm] = useState({
    id: "",
    name: "",
    userId: "",
    isDefault: false,
  });

  const navigate = useNavigate();
  const location = useLocation();

  const { userId } = useParams();

  useEffect(() => {
    if (relation === -1) {
      getUser();
    }
  }, [collections]);

  useEffect(() => {
    getUser();
  }, [userId]);

  const getUser = async () => {
    try {
      const response = await axiosPrivate.get(`/user/details/${userId}`);
      var data = response.data.result;
      setUser(data.user);
      setRelation(data.relationOption);
      setCollections(data.collections);
    } catch (err) {
      if (err.name === "CanceledError") {
        return;
      }

      navigate("/login", { state: { from: location }, replace: true });
    }
  };

  const saveFile = (e) => {
    setFile(e.target?.files[0]);
  };

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
      } else {
        console.log(response.data);
        toast.error(
          JSON.stringify(response.data.errors).replace(/[{}[\]"]/g, " ")
        );
      }
    }
  };

  const handleEditShow = async (id) => {
    const result = await axiosPrivate.get(`collection/getById/${id}`);
    if (result.data.succeeded) {
      setForm(result.data.result);
      setEditShow(true);
    }
  };

  const handleEditProfileClose = async () => {
    setEditProfileShow(false);
  };

  const handleEditProfileShow = async (id) => {
    setEditProfileShow(true);
  };

  const handleEditClose = async () => {
    setEditShow(false);
  };

  const handleAdd = async (e) => {
    e.preventDefault();
    try {
      const result = await axiosPrivate.post(`collection/create`, form);
      if (result.data.succeeded) {
        toast.success(t("collection.addedMes"));
        setAddShow(false);
        getUser();
      }
    } catch (err) {}
  };

  const handleAddShow = async () => {
    setAddShow(true);
    cleanForm();
  };

  const handleAddClose = async () => {
    setAddShow(false);
  };

  const handleEdit = async (e, id) => {
    e.preventDefault();
    try {
      const response = await axiosPrivate.put(`collection/${id}`, form);
      if (response.data.succeeded) {
        getUser();
        setEditShow(false);
        toast.success(t("collection.updatedMes"));
        cleanForm();
      }
    } catch (err) {}
  };

  const handleEditProfile = async (e) => {
    e.preventDefault();
    const formData = new FormData();
    formData.append("id", userId);
    formData.append("previousPhotoPath", user.photoPath);
    formData.append("avatar", file);
    const response = await axiosPrivate.put("user", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
    if (response.data.succeeded) {
      handleEditProfileClose();
      updateUser();
      toast.success(t("userDetails.editedProfileMes"));
    } else {
      response.data.errors.map((e) => {
        toast.error(e);
      });
    }
  };

  const onChange = (e) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  };

  const cleanForm = () => {
    setForm({
      id: "",
      name: "",
      userId: "",
      isDefault: false,
    });
  };

  const refreshUserRelationsCount = async () => {
    const response = await axiosPrivate.put(`settings/userRelations`);
    if (response.data.succeeded) {
      updateUser();
      toast.success(t("adminPanel.followersUpdateMes"));
    } else {
      response.data.errors.map((e) => {
        toast.error(e);
      });
    }
  };

  const refreshAverageRating = async () => {
    const response = await axiosPrivate.put(`settings/averageRating`);
    if (response.data.succeeded) {
      toast.success(t("adminPanel.averageRatingMes"));
    } else {
      response.data.errors.map((e) => {
        toast.error(e);
      });
    }
  };

  return (
    <>
      <div className="d-flex justify-content-center align-items-center mb-3 flex-wrap">
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
              <button
                className="btn btn-primary btn-sm follow"
                onClick={handleEditProfileShow}
              >
                {t("userDetails.editProfile")}
              </button>
            ) : (
              <RelationButton
                relation={relation}
                userId={user.id}
                userUpdate={updateUser}
              />
            )}

            <div className="d-flex justify-content-between align-items-center mt-4 px-4">
              <div className="stats">
                <h6 className="mb-0">{t("user.followers")}</h6>
                <span>
                  <Link to={`/followers/${user.id}`}>
                    {user.followersCount}
                  </Link>
                </span>
              </div>
              <div className="stats">
                <h6 className="mb-0">{t("user.friends")}</h6>
                <span>
                  <Link to={`/friends/${user.id}`}>{user.friendsCount}</Link>
                </span>
              </div>
            </div>
          </div>
        </div>
        {user.id === auth?.id && auth?.roles?.includes("admin") && (
          <div className="p-lg-4 py-4" style={{ width: "300px" }}>
            <h4>{t("adminPanel.name")}</h4>
            <div>
              <button
                className="btn btn-dark btn-sm opacity-75 my-1 me-1"
                onClick={refreshUserRelationsCount}
              >
                {t("user.followers")} / {t("user.friends")}{" "}
                <FontAwesomeIcon
                  icon="fa-solid fa-arrows-rotate"
                  className="ms-1"
                />
              </button>
              <button className="btn btn-dark btn-sm opacity-75 my-1"
              onClick={refreshAverageRating}>
                {t("adminPanel.averageRatings")}{" "}
                <FontAwesomeIcon
                  icon="fa-solid fa-arrows-rotate"
                  className="ms-1"
                />
              </button>
            </div>
          </div>
        )}
      </div>

      {collections.length === 0 ? (
        user.id === auth?.id ? (
          <h5>
            {t("userDetails.currentUser.noCollections")} <br />
            {t("forms.add")}
            <span onClick={handleAddShow} style={{ cursor: "pointer" }}>
              <FontAwesomeIcon
                icon="fa-solid fa-square-plus"
                className="ms-2 opacity-75"
              />
            </span>
          </h5>
        ) : (
          <h5>{user.userName} {t("userDetails.differentUser.noCollections")}</h5>
        )
      ) : user.id === auth?.id ? (
        <div className="d-flex">
          <h3>
          {t("userDetails.currentUser.collections")}{" "}
            <span onClick={handleAddShow} style={{ cursor: "pointer" }}>
              <FontAwesomeIcon
                icon="fa-solid fa-square-plus"
                className="ms-2 opacity-75"
              />
            </span>
          </h3>
        </div>
      ) : (
        <h3>{user.userName}{t("userDetails.differentUser.collections")}</h3>
      )}
      <div className="row">
        {collections.map((item) => (
          <div className="col-md-4 col-sm-6 mb-3" key={item.id}>
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
                      onClick={() => handleEditShow(item.id)}
                    >
                      {t("forms.edit")}
                    </Link>
                    <button
                      className="btn btn-light "
                      onClick={() => handleDelete(item.id)}
                    >
                      {t("forms.delete")}
                    </button>
                  </div>
                )}
              </div>
            </div>
          </div>
        ))}
      </div>

      <Modal show={editShow} onHide={handleEditClose}>
        <Modal.Header closeButton>
          <Modal.Title>{t("forms.edit")} {t("collection.what")}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form onSubmit={(e) => handleEdit(e, form.id)}>
            <Form.Group className="mb-3" controlId="formBasicName">
              <Form.Label>{t("collection.name")} </Form.Label>
              <Form.Control
                placeholder={t("forms.enter") + t("forms.name")}
                name="name"
                value={form.name}
                onChange={onChange}
              />
            </Form.Group>
            <Button type="submit">{t("forms.edit")}</Button>
          </Form>
        </Modal.Body>
      </Modal>

      <Modal show={addShow} onHide={handleAddClose}>
        <Modal.Header closeButton>
          <Modal.Title>{t("forms.add")} {t("collection.what")}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form onSubmit={(e) => handleAdd(e, form.id)}>
            <Form.Group className="mb-3" controlId="formBasicName">
              <Form.Label>{t("collection.name")}</Form.Label>
              <Form.Control
                placeholder={t("forms.enter") + t("forms.name")}
                name="name"
                value={form.name}
                onChange={onChange}
              />
            </Form.Group>
            <Button type="submit">{t("forms.add")}</Button>
          </Form>
        </Modal.Body>
      </Modal>

      <Modal show={editProfileShow} onHide={handleEditProfileClose}>
        <Modal.Header closeButton>
          <Modal.Title>{t("userDetails.editProfile")}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form onSubmit={(e) => handleEditProfile(e)}>
            <Form.Group className="mb-3" controlId="formBasicName">
              <Form.Control type="file" onChange={saveFile} />
            </Form.Group>
            <Form.Group className="mb-3" controlId="formBasicName">
              <Form.Label>{t("auth.username")}</Form.Label>
              <Form.Control value={user.userName} disabled />
            </Form.Group>
            <Form.Group className="mb-3" controlId="formBasicName">
              <Form.Label>{t("auth.email")}</Form.Label>
              <Form.Control value={user.email} disabled />
            </Form.Group>
            <Button type="submit">{t("form.edit")}</Button>
          </Form>
        </Modal.Body>
      </Modal>
    </>
  );
};

export default UserDetails;
