import { Page, FrameLocator, Locator, expect, Frame } from '@playwright/test';
import { AnnotationPanel } from './AnnotationPanel';
import { HelperUtil } from '../../../utils/HelperUtil';

export enum AnnotationType {
  ReplaceText = 'ReplaceText',
  InsertText = 'InsertText',
  Callout = 'Callout',
  // Add other types as needed
}

export class AnnotationViewer {
  private page: Page;
  private avFrame!: FrameLocator;
  private avPanel!: AnnotationPanel; 
  
  get annotationPanel(): AnnotationPanel {
    return this.avPanel;
  }

  constructor(page: Page) {
    this.page = page;
  }

  async waitForCanvasLoaded(setDefaultZoom = false, timeOut = 180000, isAudioVideo = false): Promise<void> {
    await this.page.locator("iframe[id^='webviewer-']").waitFor({state: 'visible', timeout: timeOut});
    this.avFrame = this.page.frameLocator("iframe[id^='webviewer-']");

    for (let i = 0; i < 10; i++)
    {
      let closedLoadSpin = this.avFrame.locator(".Modal.ProgressModal.closed");
      if(await HelperUtil.isElementExist(closedLoadSpin)) {
        break;
      }

      let loadSpin = this.avFrame.locator(".Modal.ProgressModal.open");
      if (await HelperUtil.isElementExist(loadSpin)) {
        await new Promise(resolve => setTimeout(resolve, 3000));
      }
    }

    this.avPanel = new AnnotationPanel(this.page, this.avFrame);
  }

  async selectAnnotationType(annotationType: AnnotationType): Promise<void> {
    let buttonSelector = '';
    if (annotationType === AnnotationType.ReplaceText || annotationType === AnnotationType.InsertText) {
      const prefix = annotationType.charAt(0).toUpperCase() + annotationType.slice(1);
      buttonSelector = `button[data-element='mark${prefix}GroupButton']`;
    } else {
      const prefix = annotationType.charAt(0).toLowerCase() + annotationType.slice(1);
      buttonSelector = `button[data-element='${prefix}ToolGroupButton']`;
    }

    const button = this.avFrame.locator(buttonSelector);
    if (!(await button.getAttribute('class'))?.includes('active')) {
      await button.click();
    }
  }

  async createAnnotation(pageNumber: number, x: number, y: number): Promise<void> {
    const canvasLocator = this.avFrame.locator(`#pageContainer${pageNumber} canvas`);
    const boundingBox = await canvasLocator.boundingBox();
    if (!boundingBox) throw new Error("Canvas not found or not visible");

    await canvasLocator.click({ 
      position: { x: x, y: y },
      force: true,
    });
  }

  async selectAnnotationColor(color: number): Promise<void> {
    await this.avFrame.locator("//div[@class='tool-button-arrow-inner-container']").click();
    await this.avFrame.locator(`//button[@aria-label='Color ${color}']`).click();
    await this.avFrame.locator("//div[@class='tool-button-arrow-inner-container']").click();
  }

  async addAnnotationMessage(pageNumber: number, message: string, isReply = false, willSubmit = false, annotationIndex = 0): Promise<void> {
    await this.avPanel.clickAnnotation();
    const replyBox = this.avFrame.locator("div.comment-textarea div.ql-editor p");
    await replyBox.fill(message);

    if (willSubmit) {
      const submitBtn = this.avFrame.locator("//button[contains(@class, 'reply-button') or contains(@class, 'save-button')]");
      if (!(await submitBtn.getAttribute('class'))?.includes('disabled')) {
        await submitBtn.click();
      } else {
        throw new Error("Button is disabled");
      }
    }
  }

  async deleteAllAnnotations(): Promise<void> {
    try {
      await this.avPanel.clickMultiSelect();
      await this.avPanel.clickSelectAll();
      const checked = this.avFrame.locator("//div[@data-element='notesPanel']//span[@class='ui__choice__input']");
      if (await checked.count() > 0) {
        await this.avPanel.clickMultiSelectDelete();
      }
    } catch (e) {
      throw e;
    }
  }

  async reloadPage(setDefaultZoom = false, timeOut = 60000, isAudioVideo = false): Promise<void> {
    await this.page.reload();
    await this.waitForCanvasLoaded(setDefaultZoom, timeOut, isAudioVideo);
  }
}