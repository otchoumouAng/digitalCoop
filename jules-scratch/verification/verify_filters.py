from playwright.sync_api import sync_playwright

def run(playwright):
    browser = playwright.chromium.launch(headless=True)
    context = browser.new_context()
    page = context.new_page()

    try:
        # Navigate to the OrdreFabrication page
        page.goto("http://localhost:2420/OrdreFabrication", timeout=60000)

        # Click the filter button to expand the criteria panel
        page.locator("#btnFilterOrdreFabrication").click()

        # Wait for the panel to be visible
        page.wait_for_selector("#OrdreFabricationCriteriaPanel", state="visible")

        # Take a screenshot of the filter panel
        page.locator("#OrdreFabricationCriteriaPanel").screenshot(path="jules-scratch/verification/filters.png")

    except Exception as e:
        print(f"An error occurred: {e}")
        page.screenshot(path="jules-scratch/verification/error.png")

    finally:
        browser.close()

with sync_playwright() as playwright:
    run(playwright)