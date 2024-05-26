import { WebsiteLayout } from "presentation/layouts/WebsiteLayout";
import { Fragment, memo } from "react";
import { Box } from "@mui/system";
import { Seo } from "@presentation/components/ui/Seo";
import { ContentCard } from "@presentation/components/ui/ContentCard";
import { FeedbackForm } from "@presentation/components/forms/Feedback/FeedbackForm";
import { FormattedMessage, useIntl } from "react-intl";

export const FeedbackPage = memo(() => {
  const { formatMessage } = useIntl();

  return <Fragment>
    <Seo title="MobyLab Web App | Feedback" />
    <WebsiteLayout>
      <Box sx={{ padding: "0px 50px 00px 50px", justifyItems: "center", width: '100%' }}>
        <ContentCard title={formatMessage({ id: "globals.feedback" })}>
          <FeedbackForm />
        </ContentCard>
      </Box>
    </WebsiteLayout>
  </Fragment>
});
