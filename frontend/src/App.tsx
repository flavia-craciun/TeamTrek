import { UserRoleEnum } from "@infrastructure/apis/client";
import { useOwnUserHasRole } from "@infrastructure/hooks/useOwnUser";
import { AppIntlProvider } from "@presentation/components/ui/AppIntlProvider";
import { ToastNotifier } from "@presentation/components/ui/ToastNotifier";
import { HomePage } from "@presentation/pages/HomePage";
import { LoginPage } from "@presentation/pages/LoginPage";
import { FeedbackPage } from "@presentation/pages/FeedbackPage";
import { UsersPage } from "@presentation/pages/UsersPage";
import { ProjectsPage } from "@presentation/pages/ProjectsPage";
import { Route, Routes } from "react-router-dom";
import { AppRoute } from "routes";
import { QuestionsPage } from "@presentation/pages/QuestionsPage";

export function App() {
  const isAdmin = useOwnUserHasRole(UserRoleEnum.Admin);

  return <AppIntlProvider> {/* AppIntlProvider provides the functions to search the text after the provides string ids. */}
      <ToastNotifier />
      {/* This adds the routes and route mappings on the various components. */}
      <Routes>
        <Route path={AppRoute.Index} element={<HomePage />} /> {/* Add a new route with a element as the page. */}
        <Route path={AppRoute.Login} element={<LoginPage />} />
        {isAdmin && <Route path={AppRoute.Users} element={<UsersPage />} />} {/* If the user doesn't have the right role this route shouldn't be used. */}
        <Route path={AppRoute.Projects} element={<ProjectsPage />}  />
        <Route path={AppRoute.Questions} element={<QuestionsPage />} />
        <Route path={AppRoute.Feedback} element={<FeedbackPage />} />
      </Routes>
    </AppIntlProvider>
}
