<template>
  <div class="main-container">
    <div class="grid-list-data sticky left-0">
      <div class="grid-button-list">
        <div class="flex">
          <div class="flex flex-grow">
            <div class="check-all-arrow">
              <div class="mi mi-24"></div>
            </div>

            <div id="filter-dropdown">
              <div class="dropdown-secondary ms-dropdown">
                <!----><button
                  name="button"
                  shortkey-target=""
                  class="ms-component ms-button expand-more-button ms-button-secondary ms-button ms-con-dropdown-radius-true-true expand-more-button"
                >
                  <!---->
                  <div class="ms-button--text flex align-center">
                    <span class="pr-4">Lọc</span
                    ><!---->
                    <div class="mi mi-16 mi-arrow-up--black">&nbsp;</div>
                  </div>
                </button>
              </div>
            </div>
            <div class="filter-conditions">
              <div class="filter-item--default">Đầu năm tới hiện tại</div>
            </div>
          </div>
          <div class="flex">
            <div class="ultility-buttons">
              <div class="flex search-wrapper">
                <div class="ms-input">
                  <!---->
                  <div>
                    <div
                      class="ms-component ms-con-input-label ms-input style-label"
                    >
                      <!----><span
                        ><div class="ms-con-input">
                          <input
                            placeholder="Nhập từ khóa tìm kiếm"
                            type="text"
                            class="ms-input--input hasIcon icon-after-input ms-input-normal"
                          /><!---->
                          <div class="con-ms-tooltip">
                            <div class="msTooltipParent"></div>
                            <div class="tooltip-content">
                              <div
                                class="ms-input--icon icon-after mi-search mi mi-16"
                              ></div>
                            </div>
                          </div>
                          <!----><!---->
                        </div></span
                      ><span></span>
                    </div>
                  </div>
                </div>
              </div>

              <div class="list-button">
                <div class="add-button ms-dropdown">
                  <button
                    @click="goToPaymentPage"
                    name="button"
                    class="ms-component ms-button ms-button-primary ms-button ms-radius-true ms-dropdown-style-default ms-radius-true"
                  >
                    <!---->
                    <div class="ms-button--text flex align-center">
                      <div class="con-ms-tooltip">
                        <div class="msTooltipParent"></div>
                        <div class="tooltip-content">Thêm</div>
                      </div>
                    </div></button
                  ><button
                    name="button"
                    class="ms-component ms-button expand-more-button ms-button-primary ms-button ms-con-dropdown-radius-false-true ms-padding-is-single-false-size-default expand-more-button"
                  >
                    <!---->
                    <div class="ms-button--text flex align-center">
                      <div class="line"></div>
                      <div class="mi mi-16 mi-arrow-up--white bg-green">
                        &nbsp;
                      </div>
                    </div>
                  </button>
                </div>
              </div>
              <!---->
              <div class="flex">
                <div class="guide-tour list-button">
                  <div class="con-ms-tooltip">
                    <div class="msTooltipParent"></div>
                    <div class="tooltip-content">
                      <div class="mi mi-24 mi-tour"></div>
                    </div>
                  </div>
                </div>
                <div class="list-button">
                  <div class="con-ms-tooltip">
                    <div class="msTooltipParent"></div>
                    <div class="tooltip-content">
                      <div class="mi mi-24 mi-refresh"></div>
                    </div>
                  </div>
                </div>
                <div class="list-button">
                  <div class="con-ms-tooltip">
                    <div class="msTooltipParent"></div>
                    <div class="tooltip-content">
                      <div class="mi mi-24 mi-excel__nav"></div>
                    </div>
                  </div>
                </div>
                <div class="con-ms-tooltip">
                  <div class="msTooltipParent"></div>
                  <div class="tooltip-content">
                    <div class="list-button">
                      <div class="mi mi-24 mi-setting__list"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div>
        <!---->
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "GridButtonList",
  data() {
    return {};
  },
  methods: {
    goToPaymentPage() {
      this.$router.push("/payment");
    },
  },
};
</script>

<style scoped>
.main-container {
  background-color: #fff;
  height: 90%;
}
.grid-list-data {
  margin-right: 16px;
  margin-top: 0;
}
.grid-list-data {
  background: #fff;
  padding: 12px 0 12px 16px;
  position: relative;
  margin-right: 0 !important;
}
.left-0 {
  left: 0;
}
.sticky {
  position: sticky !important;
}
.grid-list-data .grid-button-list div.flex {
  align-items: center;
}
.flex {
  display: flex !important;
}
.flex-grow {
  flex-grow: 1 !important;
}
.grid-list-data .grid-button-list #filter-dropdown {
  display: inline-block;
}
.ms-dropdown {
  display: flex;
}
.dropdown-secondary .ms-button.ms-button-secondary {
  border: 2px solid #3b3c3f;
}
[data-v-934d572c] .dropdown-secondary .ms-button.ms-button-secondary {
  border: 2px solid #3b3c3f;
}
.dropdown-secondary .ms-component.ms-button {
  padding: 5px 18px;
  background-color: #fff;
}
.dropdown-secondary .ms-component.ms-button:hover {
  background-color: #ddd;
}
.ms-con-dropdown-radius-true-true {
  border-radius: 30px;
  color: #111;
}
.ms-button .ms-button--text {
  font-weight: 600;
  position: relative;
  color: inherit;
  display: inline-block;
  transition: all 0.25s ease;
  white-space: nowrap;
  font-size: 13px;
  line-height: 13px;
}
.align-center {
  justify-content: center;
  align-items: center;
}
.pr-4 {
  padding-right: 4px !important;
}
.mi {
  background: url(https://actaspcdng1.misacdn.net/assets/Sprites-11d892c3.svg)
    no-repeat;
  cursor: pointer;
}
.mi-arrow-up--black {
  background-position: -560px -359px;
}
.mi-16 {
  width: 16px;
  height: 16px;
  min-width: 16px;
  min-height: 16px;
}
.filter-conditions .filter-item--default {
  padding-left: 8px;
  font-family: AvertaStdCY, Helvetica, Arial, sans-serif;
  font-size: 14px;
}
.grid-list-data .grid-button-list .ultility-buttons {
  justify-content: flex-end;
  display: flex;
  align-items: center;
  padding-right: 10px;
}

.grid-list-data .grid-button-list .search-wrapper {
  width: 200px;
  padding: 0 4px;
}
.ms-input .ms-input--input.hasIcon.icon-after-input {
  padding-left: 10px;
}
.grid-list-data .grid-button-list .search-wrapper input {
  height: 32px;
}
.ms-input .ms-input--input.icon-after-input {
  padding-right: 2.75rem;
}
.ms-input .ms-con-input-label {
  position: relative;
  width: 100%;
}
.style-label {
  color: #111;
}
.ms-input .ms-con-input-label .ms-con-input {
  display: flex;
  align-items: flex-start;
  flex-direction: column;
  position: relative;
  justify-content: center;
}
.ms-input .ms-input--input {
  font-size: 13px;
  height: var(--input-height);
  color: inherit;
  position: relative;
  padding: 0 10px;
  border-radius: 2px;
  border: 1px solid #babec5;
  box-sizing: border-box;
  width: 100%;
}
.ms-input-normal {
  width: 100%;
  height: 100%;
  background-color: #fff;
  outline: none;
}
input::placeholder {
  font-style: italic;
}

.con-ms-tooltip .tooltip-content {
  height: 100%;
}
.ms-input .ms-input--icon.icon-after {
  left: auto;
  right: 12px;
  border-right: 0;
}
.ms-input .ms-input--icon {
  cursor: pointer;
  position: absolute;
  left: 5px;
  font-size: 1.1rem;
  border-right: 1px solid rgba(0, 0, 0, 0.1);
  padding-right: 3px;
  color: #0006;
  -webkit-user-select: none;
  -moz-user-select: none;
  user-select: none;
  top: 6px;
}
.mi-search {
  background-position: -992px -360px;
}
.mi-16 {
  width: 16px;
  height: 16px;
  min-width: 16px;
  min-height: 16px;
}
.grid-list-data .grid-button-list .ultility-buttons .list-button {
  padding: 0 4px;
}
.add-button {
  align-items: center;
}
.ms-dropdown {
  display: flex;
}
.ms-radius-true {
  border-radius: 30px 0 0 30px;
}
.ms-dropdown-style-default {
  padding: 8px 12px 8px 16px;
  height: var(--rounded-button-height);
}
.ms-button-primary {
  border: 1px solid transparent;
  color: #fff;
  background-color: #2ca01c;
}
.add-button .ms-padding-is-single-false-size-default {
  padding: 8px 15px 5px 6px;
  border-left-color: #fff;
}
.ms-padding-is-single-false-size-default {
  width: 36px;
  height: 30px;
}
.ms-con-dropdown-radius-false-true {
  border-radius: 0 30px 30px 0;
}

.expand-more-button {
  height: 100%;
  position: relative;
}
.ms-button .ms-button--text {
  font-weight: 600;
  position: relative;
  color: inherit;
  display: inline-block;
  transition: all 0.25s ease;
  white-space: nowrap;
  font-size: 13px;
  line-height: 13px;
}
.bg-green {
  background-color: transparent;
}
.mi-arrow-up--white {
  background-position: -848px -359px;
}
.mi-16 {
  width: 16px;
  height: 16px;
  min-width: 16px;
  min-height: 16px;
}
.grid-list-data .grid-button-list .ultility-buttons .list-button {
  padding: 0 4px;
}
.guide-tour {
  display: flex;
  height: auto;
  z-index: 2;
  opacity: 0.5;
}
.mi-tour {
  background-position: -984px -144px;
}
.mi-24 {
  width: 24px;
  height: 24px;
  min-width: 24px;
  min-height: 24px;
}
.mi-refresh {
  background-position: -424px -200px;
}
.mi-excel__nav {
  background-position: -704px -200px;
}
.mi-setting__list {
  background-position: -88px -200px;
}
.ms-button-primary:hover {
  background-color: #35bf22;
}
</style>
