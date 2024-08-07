<template>
  <div class="d-flex">
    <div class="ms-tabs con-ms-tabs router-list ms-tabs-position-bottom">
      <div class="con-ms-ul-tabs flex">
        <ul class="ul-tabs ms-tabs-ul ms-ul-tabs-left">
          <li class="ms-tabs-li">
            <div class="line-top"></div>
            <div type="button" class="ms-tabs-btn">
              <div class="ms-label tab-label">
                Thu, chi tiền
                <!---->
              </div>
            </div>
          </li>
          <li class="ms-tabs-li">
            <div class="line-top"></div>
            <div type="button" class="ms-tabs-btn">
              <div class="ms-label tab-label">
                Thu tiền
                <!---->
              </div>
            </div>
          </li>
          <li class="ms-tabs-li active-item active-item-bottom">
            <div class="line-top"></div>
            <div type="button" class="ms-tabs-btn">
              <div class="ms-label tab-label">
                Chi tiền
                <!---->
              </div>
            </div>
          </li>
          <li class="ms-tabs-li">
            <div class="line-top"></div>
            <div type="button" class="ms-tabs-btn">
              <div class="ms-label tab-label">
                Ngân hàng điện tử
                <!---->
              </div>
            </div>
          </li>
          <li class="ms-tabs-li">
            <div class="line-top"></div>
            <div type="button" title="Nhập sao kê từ Excel" class="ms-tabs-btn">
              <div class="ms-label tab-label">
                Nhập sao kê từ Excel
                <!---->
              </div>
            </div>
          </li>
          <li class="ms-tabs-li">
            <div class="line-top"></div>
            <div type="button" class="ms-tabs-btn">
              <div class="ms-label tab-label">
                Khế ước đi vay
                <!---->
              </div>
            </div>
          </li>
          <li class="ms-tabs-li">
            <div class="line-top"></div>
            <div type="button" class="ms-tabs-btn">
              <div class="ms-label tab-label">
                khác
                <!---->
              </div>
              <div class="img-icon mi mi-24 mi mi-24 mi-arrow-up--black"></div>
            </div>
          </li>
        </ul>
        <!---->
        <div class="connect-eb" style="align-items: center">
          <img
            src="https://actaspcdng1.misacdn.net/assets/ic_Logo-f22ff1bb.svg"
            class="mi-24-custom"
          />
          <div class="label-connect">Kết nối ngay</div>
        </div>
        <div class="header-icon tab-header-icon">
          <div class="con-ms-tooltip">
            <div class="msTooltipParent"></div>
            <div class="tooltip-content">
              <div class="ms-dropdown">
                <!----><button>
                  <!---->
                  <div class="ms-button-text ms-button--text flex align-center">
                    <!----><!----><a class="flex items-center"
                      ><div
                        class="mi mi-24 mi mi-24 mi-setting__list pointer"
                      ></div
                    ></a>
                  </div>
                </button>
              </div>
            </div>
          </div>
          <div class="flex">
            <div class="con-ms-tooltip">
              <div class="msTooltipParent"></div>
              <div class="tooltip-content">
                <div
                  class="img-icon mi mi-24 mi mi-24 mi-separate-to-master-detail"
                ></div>
              </div>
            </div>
          </div>
        </div>
        <div class="smart-alert" style="display: none">
          <div class="con-ms-tooltip">
            <div class="msTooltipParent">
              <div
                class="ms-tooltip"
                style="left: 0px; top: 0px; position: fixed; display: none"
              >
                Gợi ý, nhắc nhở hôm nay
              </div>
            </div>
            <div class="tooltip-content">
              <div
                alt="ShowAlert"
                class="icon-hint mi mi-24 hover-effect"
              ></div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "RouterList",
  data() {
    return {};
  },
  methods: {},
};
</script>

<style scoped>
.d-flex {
  display: flex;
  position: relative;
}

.router-list {
  z-index: 59;
  padding: 6px 0 0;
}

.ms-tabs-position-bottom {
  position: sticky;
  width: 100%;
}

.con-ms-tabs {
  overflow: hidden;
  background: #eeeff1;
  z-index: 0;
  font-family: AvertaStdCY, Helvetica, Arial, sans-serif;
  height: 55px;
}

.con-ms-tabs .con-ms-ul-tabs {
  position: relative;
  display: flex;
  justify-content: space-between;
  border-bottom: 1px solid #d4d7dc;
}

.flex {
  display: flex !important;
}

.ms-tabs-position-bottom .con-ms-ul-tabs .ms-tabs-ul {
  width: auto;
  height: 100%;
  padding-left: 0;
  position: relative;
  display: flex;
  list-style-type: none; /* Hide bullet points */
}

.con-ms-tabs .con-ms-ul-tabs .ms-tabs-ul {
  margin: 0;
  padding: 0;
  height: 25px;
}

.ms-tabs-position-bottom .con-ms-ul-tabs .ms-tabs-ul .ms-tabs-li {
  border-left: none;

  height: var(--tab__router-height);
  flex: 0 1 auto; /* Evenly space the tabs */
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 0 5px; /* Adjust space between tabs */
}

.con-ms-tabs .con-ms-ul-tabs .ms-tabs-ul .ms-tabs-li {
  border-left: 6px solid transparent;
  position: relative;
}

.ms-tabs-position-bottom .con-ms-ul-tabs .ms-tabs-ul .ms-tabs-li .line-top {
  width: 100%;
  height: 4px;
  background-color: unset;
  border-radius: 5px 5px 0 0;
}

.ms-tabs-position-bottom .con-ms-ul-tabs .ms-tabs-ul .ms-tabs-li .ms-tabs-btn {
  margin: 0;
  padding: 0 12px;
  height: calc(100% - 4px);
  display: flex;
  align-items: center;
  justify-content: center;
  border-radius: unset;
}

.con-ms-tabs .con-ms-ul-tabs .ms-tabs-ul .ms-tabs-li .ms-tabs-btn {
  display: flex;
  position: relative;
  width: 100%;
  background: 0 0;
  border: 0;
  border-radius: 5px;
  cursor: pointer;
  transition: all 0.2s ease;
  z-index: 0;
  font-size: 13px;
  color: #111;
  text-align: center;
}

.ms-label {
  text-align: center;
  white-space: nowrap;
  font-size: 13px;
}

.other-li .ms-tabs-btn {
  padding: unset !important;
  position: absolute !important;
  top: 50%;
  transform: translateY(-50%);
}

.ms-dropdown-type-simple .ms-button--text {
  font-weight: 200 !important;
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

.mi.mi-arrow-up--black {
  background-position: -560px -359px;
  margin-top: 4px;
}

.mi-16 {
  width: 16px;
  height: 16px;
  min-width: 16px;
  min-height: 16px;
}

.mi {
  background: url(https://actaspcdng1.misacdn.net/assets/Sprites-11d892c3.svg)
    no-repeat;
  cursor: pointer;
}

.ms-dropdown-type-simple {
  background: 0 0;
  color: #111;
  border: none;
}

.ms-dropdown-type-simple {
  color: #111;
}

.ms-padding-is-single-true-size-default {
  height: var(--rounded-button-height);
  padding: 8px 12px 8px 16px;
}

.expand-more-button {
  height: 100%;
  position: relative;
}

.ms-button-radius-false {
  border-radius: 3px;
}

.ms-button-size-standard,
.ms-button-size-default {
  height: var(--primary-button-height);
  padding: 6px 16px;
}

.item-fake {
  display: flex;
  flex: 1;
  border-bottom: 1px solid #d4d7dc;
}

.connect-eb {
  color: #0075c0;
  cursor: pointer;
  height: 37px;
  width: 194px;
  text-align: right;
  position: relative;
  margin-left: auto; /* Push to the far right */
}

.mi-24-custom {
  width: 142px;
}

.connect-eb .label-connect {
  position: absolute;
  bottom: 0;
  right: 0;
}

.header-icon {
  position: relative;
  display: flex;
  justify-content: center;
  align-items: center;
}

.con-ms-tooltip .tooltip-content {
  height: 100%;
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

.mi-setting__list {
  background-position: -88px -200px;
}

.mi-24 {
  width: 24px;
  height: 24px;
  min-width: 24px;
  min-height: 24px;
}

.mi-separate-to-master-detail {
  background-position: -682px -1878px;
  padding-bottom: 3px;
}

button {
  border: none;
}

/* Specific styles for 'Kết nối ngay' section */
.connect-eb .label-connect {
  color: #0075c0;
  font-size: 14px;
}

.connect-eb .label-connect:hover {
  text-decoration: underline;
}
.ms-label:hover {
  color: green;
}
</style>
