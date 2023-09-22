import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom"

const Unauthorized = () => {
    const navigate = useNavigate();
    const { t } = useTranslation();

    const goBack = () => navigate(-1);

    return (
        <section>
            <h1>{t("auth.unauthorized")}</h1>
            <br />
            <p>{t("auth.noAccess")}</p>
            <div className="flexGrow">
                <button onClick={goBack}>{t("auth.goBack")}</button>
            </div>
        </section>
    )
}

export default Unauthorized