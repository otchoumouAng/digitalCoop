from playwright.sync_api import sync_playwright, expect
import time

def run(playwright):
    browser = playwright.chromium.launch(headless=True)
    context = browser.new_context()
    page = context.new_page()

    try:
        # Wait for the server to start
        time.sleep(10)

        # Navigate to the login page
        page.goto("http://localhost:2420/Login")

        # Fill in the username and password
        page.fill("input[name='Username']", "admin")
        page.fill("input[name='Password']", "123")

        # Click the login button
        page.click("button[type='submit']")

        # Wait for navigation to the workspace
        expect(page).to_have_url("http://localhost:2420/Workspace/Index")

        # Click on the "Ordre de fabrication" menu item
        page.click("a[href*='/OrdreFabrication/Index']")

        # Wait for the "Ordre de fabrication" page to load
        expect(page).to_have_url("http://localhost:2420/OrdreFabrication/Index")

        # Click the "Nouveau" button
        page.click("#btnNewOrdreFabrication")

        # Wait for the modal to appear
        expect(page.locator("#FormOrdreFabrication")).to_be_visible()

        # Take a screenshot of the modal
        page.screenshot(path="jules-scratch/verification/verification.png")

    finally:
        browser.close()

with sync_playwright() as playwright:
    run(playwright)