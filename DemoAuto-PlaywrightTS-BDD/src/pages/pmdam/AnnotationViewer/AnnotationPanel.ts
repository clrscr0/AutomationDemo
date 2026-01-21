import { Page, FrameLocator, Locator, expect, Frame } from '@playwright/test';
import { AnnotationViewer } from "./AnnotationViewer";

export class AnnotationPanel {
  private page: Page;
  private avFrame!: FrameLocator;
  private avViewer!: AnnotationViewer; 

  constructor(page: Page, avFrame: FrameLocator)
  {
    this.page = page;
    this.avViewer = new AnnotationViewer(page);
    this.avFrame = avFrame;
  }

  async clickAnnotation(annotationItem = 0, skipSwitchToPdfTron = false): Promise<void> {
    const selector = annotationItem > 0
      ? `(//div[@role='listitem' and contains(@class,'note-wrapper')])[${annotationItem}]`
      : `(//div[@role='listitem' and contains(@class,'note-wrapper')])[last()]`;

    const annotation = this.avFrame.locator(`xpath=${selector}`);
    await annotation.waitFor({ state: 'visible', timeout: 20000 });
    await annotation.click();
  }

  async clickAnnotationCheckBox(annotationItem = 0): Promise<void> {
    const selector = annotationItem > 0
      ? `(//div[@role='listitem' and contains(@class,'note-wrapper')])[${annotationItem}]//input[@type='checkbox']`
      : `(//div[@role='listitem' and contains(@class,'note-wrapper')])[last()]//input[@type='checkbox']`;

    const checkbox = this.avFrame.locator(`xpath=${selector}`);
    await checkbox.waitFor({ state: 'visible' });
    await checkbox.click();
  }

  async clickMultiSelect(): Promise<void> {
    const button = this.avFrame.locator("xpath=//button[@data-element='multiSelectModeButton' and not(contains(@class, 'active'))]");
    if (await button.isVisible()) await button.click();
  }

  async clickSelectAll(): Promise<void> {
    const button = this.avFrame.locator("xpath=//button[@id='selectAllNotesButton' and not(contains(@class, 'active'))]");
    if (await button.isVisible()) {
      await button.press('Escape');
      await button.click();
    }
  }

  async clickMultiSelectClose(): Promise<void> {
    const closeBtn = this.avFrame.locator("xpath=//div[@class='close-container']");
    await closeBtn.waitFor({ state: 'visible' });
    await closeBtn.click();
  }

  async clickMultiSelectDelete(): Promise<void> {
    const deleteBtn = this.avFrame.locator("xpath=//button[@data-element='multiDeleteButton' and not(contains(@class, 'disabled'))]");
    if (await deleteBtn.isVisible()) await deleteBtn.click();

    const confirmBtn = this.page.locator("xpath=//button[@data-element='WarningModalSignButton']");
    if (await confirmBtn.isVisible()) await confirmBtn.click();

    await this.waitForCanvasLoaded();
  }

  async waitForCanvasLoaded(): Promise<void> {
    // Implement your canvas wait logic here
  }

  async clickCancelAnnotationComment(): Promise<void> {
    const cancelBtn = this.avFrame.locator('button.cancel-button');
    if (await cancelBtn.isVisible()) await cancelBtn.click();
  }

  async click3DotsMenu(annotationItem: number, forReply = false, clickCancel = true): Promise<void> {
    await this.clickAnnotation(annotationItem);
    if (clickCancel) await this.clickCancelAnnotationComment();

    const selector = forReply
      ? "xpath=//div[@class='replies']//div[@data-element='notePopup']"
      : "xpath=//div[@class='NoteHeader parent']//div[@data-element='notePopup']";

    const menuBtn = this.avFrame.locator(selector);
    await menuBtn.click();
  }

  async verifyAnnotationColor(annotationItem: number, expectedColor: string): Promise<void> {
    const icon = this.avFrame.locator(`xpath=(//div[@role='listitem' and contains(@class,'note-wrapper')])[${annotationItem}]//div[contains(@class,'Icon type-icon')]`);
    const color = await icon.evaluate((el) => window.getComputedStyle(el).color);
    if (!color.toLowerCase().includes(expectedColor.toLowerCase())) {
      throw new Error(`Color mismatch. Expected: ${expectedColor}, Found: ${color}`);
    }
  }

  async getAnnotationCount(): Promise<number> {
    const annotations = this.avFrame.locator("div.Note");
    return await annotations.count();
  }

  async verifyAnnotationCount(expectedCount: number): Promise<void> {
    const actualCount = await this.getAnnotationCount();
    if (actualCount !== expectedCount) {
      throw new Error(`Expected count: ${expectedCount}, but found: ${actualCount}`);
    }
  }

  async updateAnnotationMessage(annotationItem: number, message: string, forReply = false, skipSave = false): Promise<void> {
    await this.click3DotsMenu(annotationItem, forReply, false);

    const editBtn = this.avFrame.locator("button[data-element='notePopupEdit']");
    await editBtn.click();

    const input = this.avFrame.locator("div.ql-editor");
    await input.press('Control+A');
    await input.fill(message);

    if (!skipSave) {
      await this.avFrame.locator("button.save-button").click();
    }
  }

  async deleteAnnotationMessage(annotationItem: number, forReply = false): Promise<void> {
    await this.click3DotsMenu(annotationItem, forReply, false);
    await this.avFrame.locator("button[data-element='notePopupDelete']").click();
  }

  async isCommentContainsInsertOrReplaceText(): Promise<boolean> {
    const replaceIcon = this.avFrame.locator("xpath=(//div[@role='listitem' and contains(@class,'note-wrapper')])//*[local-name()='svg']/*[local-name()='path' and contains(@d,'M16.5575')]");
    const insertIcon = this.avFrame.locator("xpath=(//div[@role='listitem' and contains(@class,'note-wrapper')])//*[local-name()='svg']/*[local-name()='path' and contains(@d,'M17.5575')]");
    return await replaceIcon.count() > 0 || await insertIcon.count() > 0;
  }
} 